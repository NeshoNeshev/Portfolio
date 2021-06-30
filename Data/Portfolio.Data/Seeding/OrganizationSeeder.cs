namespace Portfolio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Models;

    public class OrganizationSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var professionalExperience = dbContext.ProfessionalExperiences.FirstOrDefault(x => x.Id != null);
            if (dbContext.Organizations.Any())
            {
                return;
            }

            var organization = new Organization
            {
                Id = Guid.NewGuid().ToString(),
                OrganizationName = "Software University",
                CompanySize = "Over 300",
            };
            await dbContext.Organizations.AddAsync(organization);
            await dbContext.SaveChangesAsync();
            professionalExperience?.Organizations.Add(organization);
        }
    }
}
