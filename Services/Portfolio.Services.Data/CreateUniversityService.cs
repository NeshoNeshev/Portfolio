using System;

namespace Portfolio.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;

    public class CreateUniversityService : ICreateUniversityService
    {
        private readonly IDeletableEntityRepository<University> universityRepository;
        private readonly IDeletableEntityRepository<Country> countryRepository;
        private readonly IDeletableEntityRepository<PrivateInformation> privatEntityRepository;

        public CreateUniversityService(IDeletableEntityRepository<University> universityRepository,
            IDeletableEntityRepository<Country> countryRepository,
            IDeletableEntityRepository<PrivateInformation>privatEntityRepository)
        {
            this.universityRepository = universityRepository;
            this.countryRepository = countryRepository;
            this.privatEntityRepository = privatEntityRepository;
        }

        public async Task CreateAsync(string name, string period)
        {
            var countries = this.countryRepository.All().Where(x => x.CountryName != null).Select(x => x.Id).ToList();
            var countryId = countries[0];
            var privateInformations = this.privatEntityRepository.All().Where(x => x.FirstName == "Nesho")
                .Select(x => x.Id).ToList();
            var privateId = privateInformations[0];
            var university = new University
            {
                Id = Guid.NewGuid().ToString(),
                UniversityName = name,
                Period = period,
            };
            university.PrivateInformationId = privateId;
            university.CountryId = countryId;

            await this.universityRepository.AddAsync(university);
            await this.universityRepository.SaveChangesAsync();
        }
    }
}