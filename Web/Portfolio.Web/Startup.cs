namespace Portfolio.Web
{
    using System;
    using System.Reflection;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Portfolio.Data;
    using Portfolio.Data.Common;
    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Data.Repositories;
    using Portfolio.Data.Seeding;
    using Portfolio.Services.Data;
    using Portfolio.Services.Mapping;
    using Portfolio.Services.Messaging;
    using Portfolio.Web.ViewModels;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddDistributedMemoryCache();
            services.AddSession(options => { options.IdleTimeout = TimeSpan.FromHours(3); });
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(this.configuration);
            Account account = new Account
            (
                this.configuration["Cloudinary:AppName"],
                this.configuration["Cloudinary:AppKey"],
                this.configuration["Cloudinary:AppSecret"]);

            Cloudinary cloudinary = new Cloudinary(account);
            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();
            services.AddScoped(typeof(IChangeInputToUpper<>), typeof(ChangeInputToUpper<>));
            // Application services
            services.AddSingleton(cloudinary);
            services.AddTransient<IEmailSender>(
                serviceProvider => new SendGridEmailSender(this.configuration["SendGrid:ApiKey"]));
            services.AddTransient<IOrganizationServices, OrganizationServices>();
            services.AddTransient<ISectorService, SectorService>();
            services.AddTransient<IPositionService, PositionService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<ITownService, TownService>();
            services.AddTransient<IUniversityService, UniversityService>();
            services.AddTransient<ISpecialtiesService, SpecialtiesService>();
            services.AddTransient<ICertificatesService, CertificatesService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IGetAgeService, GetAgeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
