using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portfolio.Data.Common.Repositories;
using Portfolio.Data.Models;
using Portfolio.Services.Mapping;
using Portfolio.Web.ViewModels.Administration.Dashboard;
using Portfolio.Web.ViewModels.Administration.Speciality;

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

        public async Task CreateAsync(CreateSpecialtyInputModel model)
        {
            var university = this.universityRepository.All().FirstOrDefault(x => x.Id == model.UniversityId);
            if (university == null)
            {
                return;
            }

            var speciality = new Specialty
            {
                Id = Guid.NewGuid().ToString(),
                SpecialtyName = model.SpecialtyName,
                Degree = model.Degree,
            };
            speciality.University = university;
            await this.specialityRepository.AddAsync(speciality);
            await this.specialityRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Specialty> query = this.specialityRepository.All().OrderBy(x => x.SpecialtyName);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }
            return query.To<T>().ToList();
        }

        public bool FindByNameAsync(string name) => this.specialityRepository
            .All()
            .Any(s => s.SpecialtyName == name);

        public bool FindByIdAsync(string id) => this.specialityRepository
            .All()
             .Any(s => s.Id == id);


        public async Task UpdateAsync(EditSpecialityInputModel model)
        {
            var speciality = this.specialityRepository
                    .All()
                    .FirstOrDefault(x => x.Id == model.Id);

            speciality.SpecialtyName = model.NewSpecialtyName;

            this.specialityRepository.Update(speciality);
            await this.specialityRepository.SaveChangesAsync();
        }
    }
}