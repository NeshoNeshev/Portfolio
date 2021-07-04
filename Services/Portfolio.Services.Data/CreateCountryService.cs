namespace Portfolio.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;

    public class CreateCountryService : ICreateCountryService
    {
        private readonly IDeletableEntityRepository<Country> countryRepository;
        private readonly IDeletableEntityRepository<Town> townRepository;
        private readonly ICreateTownService createTownService;

        public CreateCountryService(IDeletableEntityRepository<Country> countryRepository,
            IDeletableEntityRepository<Town> townRepository
            ,ICreateTownService createTownService)
        {
            this.countryRepository = countryRepository;
            this.townRepository = townRepository;
            this.createTownService = createTownService;
        }

        public async Task CreateAsync(string countryName, string townName)
        {
            var exist = this.countryRepository.All().Any(n => n.CountryName == countryName);
            var existTown = this.townRepository.All().FirstOrDefault(x => x.TownName == townName);
            if (exist)
            {
                return;
            }

            if (existTown == null)
            {
                await this.createTownService.CreateAsync(townName, countryName);
            }

            var town = this.townRepository.All().FirstOrDefault(x => x.TownName == townName);
            var country = new Country
            {
                Id = Guid.NewGuid().ToString(),
                CountryName = countryName,
            };

            if (country.Towns.Contains(existTown))
            {
                return;
            }

            country.Towns.Add(town);
            await this.countryRepository.AddAsync(country);
            await this.countryRepository.SaveChangesAsync();
        }
    }
}