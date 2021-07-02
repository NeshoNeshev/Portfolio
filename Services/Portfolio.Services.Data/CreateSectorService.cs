namespace Portfolio.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;

    public class CreateSectorService : ICreateSectorService
    {
        private readonly IDeletableEntityRepository<Sector> sectoRepository;
        private readonly IDeletableEntityRepository<Organization> organizationRepository;
        private readonly IDeletableEntityRepository<Position> _positionRepository;
        private readonly ICreatePositionService _positionService;

        public CreateSectorService(IDeletableEntityRepository<Sector> sectoRepository,
            IDeletableEntityRepository<Organization>organizationRepository,IDeletableEntityRepository<Position>positionRepository,ICreatePositionService positionService)
        {
            this.sectoRepository = sectoRepository;
            this.organizationRepository = organizationRepository;
            _positionRepository = positionRepository;
            _positionService = positionService;
        }

        public async Task CreateAsync(string sectorName, string organizationName,string positionName, string positionMoreInformation, string positionPeriod)
        {
            var organization = this.organizationRepository.All()
                .FirstOrDefault(x => x.OrganizationName == organizationName);
            var existPosition = this._positionRepository.All().Any(x => x.PositionName == positionName);
            if (organization == null)
            {
                return;
            }

            if (!existPosition)
            {
                await this._positionService.CreateAsync(positionName, positionMoreInformation, positionPeriod);
            }

            var position = this._positionRepository.All().FirstOrDefault(x => x.PositionName == positionName);
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

       
    }
}