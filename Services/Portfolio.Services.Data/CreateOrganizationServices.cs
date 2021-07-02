namespace Portfolio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Web.ViewModels.Administration.Dashboard;

    public class CreateOrganizationServices : ICreateOrganizationServices
    {
        private readonly IDeletableEntityRepository<PrivateInformation> privateInformationRepository;
        private readonly IDeletableEntityRepository<Position> positionRepository;
        private readonly IDeletableEntityRepository<Organization> organizationRepository;
        private readonly IDeletableEntityRepository<Country> countryRepository;
        private readonly IDeletableEntityRepository<Sector> sectorRepository;
        private readonly ICreateCountryService createCountryService;
        private readonly ICreateTownService townService;
        private readonly ICreateSectorService sectorService;
        private readonly ICreatePositionService createPosition;
        private readonly IChangeInputToUpper<OrganizationInputModel> changeInputFirstLetter;

        public CreateOrganizationServices(
            IDeletableEntityRepository<PrivateInformation> privateInformationRepository,
            IDeletableEntityRepository<Position> positionRepository,
            IDeletableEntityRepository<Organization> organizationRepository,
            IDeletableEntityRepository<Country> countryRepository,
            IDeletableEntityRepository<Sector> sectorRepository,
            ICreateCountryService createCountryService,
            ICreateTownService townService,
            ICreateSectorService sectorService,
            ICreatePositionService createPosition,
            IChangeInputToUpper<OrganizationInputModel> changeInputFirstLetter
        )
        {
            this.privateInformationRepository = privateInformationRepository;
            this.positionRepository = positionRepository;
            this.organizationRepository = organizationRepository;
            this.countryRepository = countryRepository;
            this.sectorRepository = sectorRepository;
            this.createCountryService = createCountryService;
            this.townService = townService;
            this.sectorService = sectorService;
            this.createPosition = createPosition;
            this.changeInputFirstLetter = changeInputFirstLetter;
        }
        
        public async Task CreateAsync(OrganizationInputModel input)
        {
            var privateInformation = this.privateInformationRepository.All().FirstOrDefault(x => x.FirstName == input.PrivateName);
            var country = this.countryRepository.All().FirstOrDefault(x => x.CountryName == input.CountryName);
            var organizationExist = this.organizationRepository.All().Any(x => x.OrganizationName == input.OrganizationName);
            if (organizationExist)
            {
                return;
            }

            if (privateInformation == null)
            {
                return;
            }

            List<string> forbidden = new List<string>() { input.PositionMoreInformation };
            this.changeInputFirstLetter.ToUpper(input, forbidden);
            if (country == null)
            {
                await this.createCountryService.CreateAsync(input.CountryName, input.TownName);
            }
            else
            {
                await this.townService.CreateAsync(input.TownName, input.CountryName);
            }

            var organization = new Organization
            {
                Id = Guid.NewGuid().ToString(),
                OrganizationName = input.OrganizationName,
                CompanySize = input.OrganizationSize,
            };

            organization.PrivateInformation = privateInformation;
            organization.Country = country;

            await this.organizationRepository.AddAsync(organization);
            await this.organizationRepository.SaveChangesAsync();

            var sector = this.sectorRepository.All().FirstOrDefault(x => x.SectorName == input.SectorName);

            if (sector == null)
            {
                await this.sectorService.CreateAsync(input.SectorName, input.OrganizationName, input.PositionName, input.PositionMoreInformation, input.PositionPeriod);
            }
            else
            {
                var position = this.positionRepository.All().FirstOrDefault(x => x.PositionName == input.PositionName);
                if (position == null)
                {
                    await this.createPosition.CreateAsync(input.PositionName, input.PositionMoreInformation, input.PositionPeriod);
                }

                sector.Positions.Add(position);
                organization.Sectors.Add(sector);
            }
        }

        
    }
}
