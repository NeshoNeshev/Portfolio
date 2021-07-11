namespace Portfolio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Portfolio.Common;
    using Portfolio.Data.Models;

    public class PrivateInformationSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = userManager.FindByNameAsync(GlobalConstants.AdministratorUserName);

            if (user == null)
            {
                return;
            }

            if (dbContext.PrivateInformations.Any())
            {
                return;
            }

            var privateInformation = new PrivateInformation()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Nesho",
                LastName = "Georgiev",
                ThirdName = "Neshev",
                Birthday = "31.10.1978",
                Email = "neshev1978@gmail.com",
                Gender = "Men",
                PhoneNumber = "+359 888 888 888",
                Description = GlobalConstants.UserDescription,
            };
            user.Result.PrivateInformation = privateInformation;
            await dbContext.SaveChangesAsync();
        }
    }
}
