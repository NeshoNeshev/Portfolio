using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Data.Models;

namespace Portfolio.Data.Seeding
{
    public class ProfessionalExperiencesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {

            var privateInformation = dbContext.PrivateInformations.FirstOrDefault(p => p.Id != null);

            if (dbContext.ProfessionalExperiences.Any())
            {
                return;
            }

            await dbContext.ProfessionalExperiences.AddAsync(new ProfessionalExperience
            {
                Id = Guid.NewGuid().ToString(),
                Period = "02.02.2020 - 03.03.2020",
            });
            await dbContext.SaveChangesAsync();

            var professionalExp = dbContext.ProfessionalExperiences.FirstOrDefault(x => x.Id != null);
            if (professionalExp == null)
            {
                return;
            }
            else
            {
                privateInformation.ProfessionalExperiences.Add(professionalExp);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
