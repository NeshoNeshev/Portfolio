using System.Collections.Generic;
using Portfolio.Services.Mapping;

namespace Portfolio.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Web.ViewModels.Administration.Dashboard;

    public class CreateSectorService : ICreateSectorService
    {
        private readonly IDeletableEntityRepository<Sector> sectoRepository;
        private readonly IDeletableEntityRepository<Organization> organizationRepository;
        private readonly IDeletableEntityRepository<Position> positionRepository;
        private readonly ICreatePositionService positionService;

        public CreateSectorService(IDeletableEntityRepository<Sector> sectoRepository,
            IDeletableEntityRepository<Organization>organizationRepository,IDeletableEntityRepository<Position>positionRepository,ICreatePositionService positionService)
        {
            this.sectoRepository = sectoRepository;
            this.organizationRepository = organizationRepository;
            this.positionRepository = positionRepository;
            this.positionService = positionService;
        }

        public async Task CreateAsync(string sectorName, string organizationId, string positionName, string positionMoreInformation, string positionPeriod)
        {
            var organization = this.organizationRepository.All()
                .FirstOrDefault(x => x.Id == organizationId);
            var existPosition = this.positionRepository.All().Any(x => x.PositionName == positionName);
            if (organization == null)
            {
                return;
            }

            if (!existPosition)
            {
                await this.positionService.CreateAsync(positionName,positionMoreInformation, positionPeriod);
            }
            
            var position = this.positionRepository.All().FirstOrDefault(x => x.PositionName == positionName);
            var sector = new Sector
            {
                Id = Guid.NewGuid().ToString(),
                SectorName = sectorName,
            };
            sector.Positions.Add(position);
            sector.Organization = organization;
            await this.sectoRepository.AddAsync(sector);
            await this.sectoRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Sector> query = this.sectoRepository.All().OrderBy(x => x.SectorName);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}