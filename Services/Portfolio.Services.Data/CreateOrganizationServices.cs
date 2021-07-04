using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Portfolio.Services.Mapping;

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
        private readonly IChangeInputToUpper<OrganizationInputModel> changeInputToUpper;
        private readonly UserManager<ApplicationUser> _userManager;

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
            IChangeInputToUpper<OrganizationInputModel> changeInputToUpper,
            UserManager<ApplicationUser> userManager
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
            this.changeInputToUpper = changeInputToUpper;
            _userManager = userManager;
        }

        public async Task CreateAsync(OrganizationInputModel input)
        {

            var organizationExist = this.organizationRepository.All().Any(x => x.OrganizationName == input.OrganizationName);
            if (organizationExist)
            {
                return;
            }

            var privateInformation = this.privateInformationRepository.All().FirstOrDefault(x => x.FirstName == input.PrivateName);
            if (privateInformation == null)
            {
                return;
            }

            //List<string> forbidden = new List<string>() { input.PositionMoreInformation };
            // var a =this.changeInputToUpper.ToUpper(input, forbidden);

            var country = this.countryRepository.All().FirstOrDefault(x => x.CountryName == input.CountryName);
            if (country == null)
            {
                await this.createCountryService.CreateAsync(input.CountryName, input.TownName);
            }
            else
            {
                var exist = country.Towns.Any(x => x.TownName == input.TownName);
                if (!exist)
                {
                    await this.townService.CreateAsync(input.TownName, input.CountryName);
                }
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
                await this.sectorService.CreateAsync(input.SectorName,input.OrganizationName,input.PositionName,input.PositionMoreInformation,input.PositionPeriod);
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
        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Organization> query = this.organizationRepository.All().OrderBy(x => x.OrganizationName);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}
