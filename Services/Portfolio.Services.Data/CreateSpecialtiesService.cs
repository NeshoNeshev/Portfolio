using System;
using System.Linq;
using System.Threading.Tasks;
using Portfolio.Data.Common.Repositories;
using Portfolio.Data.Models;

namespace Portfolio.Services.Data
{
    public class CreateSpecialtiesService : ICreateSpecialtiesService
    {
        private readonly IDeletableEntityRepository<Specialty> specialityRepository;
        private readonly IDeletableEntityRepository<University> universityRepository;

        public CreateSpecialtiesService(IDeletableEntityRepository<Specialty>specialityRepository,
            IDeletableEntityRepository<University>universityRepository)
        {
            this.specialityRepository = specialityRepository;
            this.universityRepository = universityRepository;
        }

        public async Task CreateAsync(string specialtyName, string degree)
        {
            var univerities = this.universityRepository.All().Where(x => x.UniversityName != null).Select(x => x.Id)
                .ToList();
            var universityId = univerities[0];

            var speciality = new Specialty
            {
                Id = Guid.NewGuid().ToString(),
                SpecialtyName = specialtyName,
                Degree = degree,
            };
            speciality.UniversityId = universityId;
            await this.specialityRepository.AddAsync(speciality);
            await this.specialityRepository.SaveChangesAsync();
        }
    }
}