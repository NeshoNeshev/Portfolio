namespace Portfolio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;
    using Portfolio.Web.ViewModels.Administration.Specialty;

    public class SpecialtiesService : ISpecialtiesService
    {
        private readonly IDeletableEntityRepository<Specialty> specialtyRepository;
        private readonly IDeletableEntityRepository<University> universityRepository;

        public SpecialtiesService(
            IDeletableEntityRepository<Specialty> specialtyRepository,
            IDeletableEntityRepository<University> universityRepository)
        {
            this.specialtyRepository = specialtyRepository;
            this.universityRepository = universityRepository;
        }

        public async Task CreateAsync(CreateSpecialtyInputModel model)
        {
            var university = this.universityRepository.All().FirstOrDefault(x => x.Id == model.UniversityId);
            if (university == null)
            {
                return;
            }

            var specialty = new Specialty
            {
                Id = Guid.NewGuid().ToString(),
                SpecialtyName = model.SpecialtyName,
                Degree = model.Degree,
            };
            specialty.University = university;
            await this.specialtyRepository.AddAsync(specialty);
            await this.specialtyRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Specialty> query = this.specialtyRepository.All().OrderBy(x => x.SpecialtyName);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public bool FindByNameAsync(string name) => this.specialtyRepository
            .All()
            .Any(s => s.SpecialtyName == name);

        public bool FindByIdAsync(string id) => this.specialtyRepository
            .All()
             .Any(s => s.Id == id);

        public async Task UpdateAsync(EditSpecialtyInputModel model)
        {
            var specialty = this.specialtyRepository
                    .All()
                    .FirstOrDefault(x => x.Id == model.Id);

            specialty.SpecialtyName = model.NewSpecialtyName;

            this.specialtyRepository.Update(specialty);
            await this.specialtyRepository.SaveChangesAsync();
        }
    }
}
