namespace Portfolio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;
    using Portfolio.Web.ViewModels.Administration.Dashboard;

    public class SectorService : ISectorService
    {
        private readonly IDeletableEntityRepository<Sector> sectorRepository;
        private readonly IDeletableEntityRepository<Organization> organizationRepository;

        public SectorService(
            IDeletableEntityRepository<Sector> sectorRepository,
            IDeletableEntityRepository<Organization> organizationRepository)
        {
            this.sectorRepository = sectorRepository;
            this.organizationRepository = organizationRepository;
        }

        public async Task CreateAsync(CreateSectorInputModel model)
        {
            var organization = this.organizationRepository.All().FirstOrDefault(x=>x.Id == model.OrganizationId);
            if (organization == null)
            {
               return;
            }

            var exist = this.sectorRepository.All().Any(x => x.SectorName == model.SectorName);
            if (exist)
            {
                return;
            }

            var sector = new Sector
            {
                Id = Guid.NewGuid().ToString(),
                SectorName = model.SectorName,
                Organization= organization,
            };

            await this.sectorRepository.AddAsync(sector);
            await this.sectorRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Sector> query = this.sectorRepository.All().Include(x=>x.Positions).OrderBy(x => x.SectorName);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public bool FindByNameAsync(string name)
            => this.sectorRepository
                .All()
                .Any(s => s.SectorName == name);

        public bool FindByIdAsync(string id) => this.sectorRepository
            .All()
            .Any(x => x.Id == id);

        public async Task<T> GetByName<T>(string name)
            => await this.sectorRepository
                .All()
                .Where(p => p.SectorName== name)
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task UpdateAsync(EditSectorInputModel input)
        {
            var sector = this.sectorRepository
                .All()
                .FirstOrDefault(x => x.Id == input.Id);

            sector.SectorName = input.NewSectorName;

            this.sectorRepository.Update(sector);
            await this.sectorRepository.SaveChangesAsync();
        }
    }
}
