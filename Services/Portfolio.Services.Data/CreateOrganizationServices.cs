using Portfolio.Web.Areas.Administration.Views.Organization;
using Portfolio.Web.ViewModels.Administration.Organization;

namespace Portfolio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;
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
            this._userManager = userManager;
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
        }

        public async Task UpdateAsync(EditOrganizationInputModel input)
        {
            var organization = this.organizationRepository
                .All()
                .FirstOrDefault(x => x.Id == input.Id);

            organization.OrganizationName = input.NewOrganizationName;
            organization.CompanySize = input.NewCompanySize;
            this.organizationRepository.Update(organization);
            await this.organizationRepository.SaveChangesAsync();
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

        public bool FindByNameAsync(string name)
            => this.organizationRepository
                .All()
                .Any(s => s.OrganizationName == name);

        public bool FindByIdAsync(string id) => this.organizationRepository
            .All()
            .Any(x => x.Id == id);
    }
}
