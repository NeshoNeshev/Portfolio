namespace Portfolio.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;

    public class TownService : ITownService
    {
        private readonly IDeletableEntityRepository<Town> townRepository;
        private readonly IDeletableEntityRepository<Country> countryRepository;

        public TownService(IDeletableEntityRepository<Town> townRepository, IDeletableEntityRepository<Country> countryRepository)
        {
            this.townRepository = townRepository;
            this.countryRepository = countryRepository;
        }

        public async Task CreateAsync(string townName, string countryName)
        {
            var country = this.countryRepository.All().FirstOrDefault(x => x.CountryName == countryName);
            var exist = this.townRepository.All().Any(x => x.TownName == townName);
            if (exist)
            {
                return;
            }

            var town = new Town
            {
                Id = Guid.NewGuid().ToString(),
                TownName = townName,
            };
            town.Country = country;

            await this.townRepository.AddAsync(town);
            await this.townRepository.SaveChangesAsync();
        }
    }
}
