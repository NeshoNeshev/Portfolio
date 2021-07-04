using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portfolio.Data.Common.Repositories;
using Portfolio.Data.Models;
using Portfolio.Services.Mapping;

namespace Portfolio.Services.Data
{
    public class CreateSpecialtiesService : ICreateSpecialtiesService
    {
        private readonly IDeletableEntityRepository<Specialty> specialityRepository;
        private readonly IDeletableEntityRepository<University> universityRepository;

        public CreateSpecialtiesService(IDeletableEntityRepository<Specialty> specialityRepository,
            IDeletableEntityRepository<University> universityRepository)
        {
            this.specialityRepository = specialityRepository;
            this.universityRepository = universityRepository;
        }

        public async Task CreateAsync(string specialtyName, string degree, string universityName)
        {
            var university = this.universityRepository.All().FirstOrDefault(x => x.UniversityName == universityName);
            if (university == null)
            {
                return;
            }

            var speciality = new Specialty
            {
                Id = Guid.NewGuid().ToString(),
                SpecialtyName = specialtyName,
                Degree = degree,
            };
            speciality.University = university;
            await this.specialityRepository.AddAsync(speciality);
            await this.specialityRepository.SaveChangesAsync();
        }
        //public IEnumerable<TModel> GetAll<TModel>()
        //    => this.specialityRepository
        //        .AllAsNoTracking()
        //        .To<TModel>()
        //        .ToList();
    }
}