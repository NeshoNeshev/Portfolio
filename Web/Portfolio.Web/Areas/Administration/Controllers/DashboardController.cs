namespace Portfolio.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Services.Data;
    using Portfolio.Web.ViewModels.Administration.Dashboard;

    public class DashboardController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Organization> organizationRepository;
        private readonly IDeletableEntityRepository<PrivateInformation> privateRepository;
        private readonly ICreateOrganizationServices organizationServices;
        private readonly ICreateSectorService sectorService;
        private readonly ICreatePositionService positionService;
        private readonly ICreateCountryService countryService;
        private readonly ICreateTownService createTownService;
        private readonly ICreateUniversityService createUniversityService;
        private readonly ICreateSpecialtiesService specialtiesService;
        private readonly ICreateCertificatesService certificatesService;

        private readonly ICreateCourseService courseService;

        public DashboardController(
            IDeletableEntityRepository<Organization> organizationRepository,
            IDeletableEntityRepository<PrivateInformation> privateRepository,
            ICreateOrganizationServices organizationServices,
            ICreateSectorService sectorService,
            ICreatePositionService positionService,
            ICreateCountryService countryService,
            ICreateTownService createTownService,
            ICreateUniversityService createUniversityService,
            ICreateSpecialtiesService specialtiesService,
            ICreateCertificatesService certificatesService,
            ICreateCourseService courseService
            )
        {
            this.organizationRepository = organizationRepository;
            this.privateRepository = privateRepository;
            this.organizationServices = organizationServices;
            this.sectorService = sectorService;
            this.positionService = positionService;
            this.countryService = countryService;
            this.createTownService = createTownService;
            this.createUniversityService = createUniversityService;
            this.specialtiesService = specialtiesService;
            this.certificatesService = certificatesService;
            this.courseService = courseService;
        }

        [Authorize]
        public IActionResult CreateOrganization()
        {

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrganization(OrganizationInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            else
            {
                var organizationName = this.organizationRepository.All().Any(x => x.OrganizationName == input.OrganizationName);
                var privateName = this.privateRepository.All().Any(x => x.FirstName == input.PrivateName);
                if (organizationName)
                {
                    this.ModelState.AddModelError(nameof(OrganizationInputModel.OrganizationName), $"Exist {input.OrganizationName}");
                    return this.View(input);
                }

                if (!privateName)
                {
                    this.ModelState.AddModelError(nameof(OrganizationInputModel.PrivateName), $"Not Found {input.PrivateName}");
                    return this.View(input);
                }
            }

            await this.organizationServices.CreateAsync(input);

            //await this.sectorService.CreateAsync(input.SectorName);

            //await this._positionService.CreateAsync(input.PositionName, input.MoreInformation);

            //await this._countryService.CreateAsync(input.CountryName);

            //await this._createTownService.CreateAsync(input.TownName);

            //await this._createUniversityService.CreateAsync(input.UniversityName, input.UniversityPeriod);

            //await this._specialtiesService.CreateAsync(input.SpecialityName, input.Degree);

            //await this._certificatesService.CreateAsync(input.CertificateName, input.Link, input.Description, input.Date);

            //await this._courseService.CreateAsync(input.CourseName, input.CourseDescription, input.CourseDescription);

            return this.View();
        }

        public IActionResult CreateUniversity()
        {
            return this.View();
        }
    }
}
