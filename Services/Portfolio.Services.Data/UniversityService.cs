using Microsoft.EntityFrameworkCore;

namespace Portfolio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;
    using Portfolio.Web.ViewModels.Administration.Dashboard;
    using Portfolio.Web.ViewModels.Administration.University;

    public class UniversityService : IUniversityService
    {
        private readonly IDeletableEntityRepository<University> universityRepository;
        private readonly IDeletableEntityRepository<Country> countryRepository;
        private readonly IDeletableEntityRepository<PrivateInformation> privatEntityRepository;
        private readonly ICountryService createCountry;
        private readonly ITownService townService;

        public UniversityService(IDeletableEntityRepository<University> universityRepository,

            IDeletableEntityRepository<Country> countryRepository,
            IDeletableEntityRepository<PrivateInformation> privatEntityRepository,
            ICountryService createCountry,
            ITownService townService,
            ICourseService courseService)
        {
            this.universityRepository = universityRepository;
            this.countryRepository = countryRepository;
            this.privatEntityRepository = privatEntityRepository;
            this.createCountry = createCountry;
            this.townService = townService;
        }

        public async Task CreateAsync(CreateUniversityInputModel model)
        {
            var privateInformation = this.privatEntityRepository.All().FirstOrDefault(x => x.FirstName == model.PrivateName);
            var universityExist = this.universityRepository.All().Any(x => x.UniversityName == model.UniversityName);
            if (universityExist)
            {
                return;
            }

            if (privateInformation == null)
            {
                return;
            }

            var country = this.countryRepository.All().FirstOrDefault(x => x.CountryName == model.CountryName);

            if (country == null)
            {
                await this.createCountry.CreateAsync(model.CountryName, model.TownName);
            }
            else
            {
                var exist = country.Towns.Any(x => x.TownName == model.TownName);
                if (!exist)
                {
                    await this.townService.CreateAsync(model.TownName, model.CountryName);
                }
            }

            var university = new University
            {
                Id = Guid.NewGuid().ToString(),
                UniversityName = model.UniversityName,
                Period = model.PeriodInUniversity,
                PrivateInformation = privateInformation,
                Country = country,
            };

            await this.universityRepository.AddAsync(university);
            await this.universityRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditUniversityInputModel input)
        {
            var university = this.universityRepository
                .All()
                .FirstOrDefault(x => x.Id == input.Id);
            university.UniversityName = input.NewUniversityName;

            this.universityRepository.Update(university);
            await this.universityRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<University> query = this.universityRepository.All().OrderBy(x => x.UniversityName).Include(x=>x.Specialties).ThenInclude(x=>x.Courses).ThenInclude(x=>x.Certificates);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public bool FindByNameAsync(string name) => this.universityRepository
            .All()
            .Any(s => s.UniversityName == name);

        public bool FindByIdAsync(string id) => this.universityRepository
            .All()
            .Any(x => x.Id == id);
    }
}