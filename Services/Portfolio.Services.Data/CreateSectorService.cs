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

    public class CreateSectorService : ICreateSectorService
    {
        private readonly IDeletableEntityRepository<Sector> sectoRepository;
        private readonly IDeletableEntityRepository<Organization> organizationRepository;
        private readonly IDeletableEntityRepository<Position> positionRepository;


        public CreateSectorService(IDeletableEntityRepository<Sector> sectoRepository,
            IDeletableEntityRepository<Organization> organizationRepository, IDeletableEntityRepository<Position> positionRepository)
        {
            this.sectoRepository = sectoRepository;
            this.organizationRepository = organizationRepository;
            this.positionRepository = positionRepository;
        }

        public async Task CreateAsync(CreateSectorInputModel model)
        {
            var organization = this.organizationRepository.All().FirstOrDefault(x=>x.Id == model.OrganizationId);
            if (organization == null)
            {
               return;
            }

            var exist = this.sectoRepository.All().Any(x => x.SectorName == model.SectorName);
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

            await this.sectoRepository.AddAsync(sector);
            await this.sectoRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Sector> query = this.sectoRepository.All().Include(x=>x.Positions).OrderBy(x => x.SectorName);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public bool FindByNameAsync(string name)
            => this.sectoRepository
                .All()
                .Any(s => s.SectorName == name);

        public bool FindByIdAsync(string id) => this.sectoRepository
            .All()
            .Any(x => x.Id == id);

        public async Task<T> GetByName<T>(string name)
            => await this.sectoRepository
                .All()
                .Where(p => p.SectorName== name)
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task UpdateAsync(EditSectorInputModel input)
        {
            var sector = this.sectoRepository
                .All()
                .FirstOrDefault(x => x.Id == input.Id);

            sector.SectorName = input.NewSectorName;

            this.sectoRepository.Update(sector);
            await this.sectoRepository.SaveChangesAsync();
        }
    }
}