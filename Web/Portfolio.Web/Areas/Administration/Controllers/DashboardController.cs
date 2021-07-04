using System.Security.Claims;
using Microsoft.CodeAnalysis.CSharp;
using Portfolio.Common;

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

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class DashboardController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Organization> organizationRepository;
        private readonly IDeletableEntityRepository<PrivateInformation> privateRepository;
        private readonly IDeletableEntityRepository<Town> _townRepository;
        private readonly IDeletableEntityRepository<University> _universityRepository;
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
            IDeletableEntityRepository<Town> townRepository,
            IDeletableEntityRepository<University> universityRepository,
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
            _townRepository = townRepository;
            _universityRepository = universityRepository;
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
        public IActionResult Index()
        {

            return this.View();
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUniversity(CreateUniversityViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                ModelState.Remove(nameof(CreateUniversityViewModel.UniversityName));
                this.ModelState.AddModelError(nameof(CreateUniversityViewModel.UniversityName),$"University first Letter to upper");
                return this.View(model);
            }
            else
            {
                var townName = this._townRepository.All().Any(x => x.TownName == model.TownName);
                var universityName = this._universityRepository.All().Any(x => x.UniversityName == model.UniversityName);
                var privateName = this.privateRepository.All().Any(x => x.FirstName == model.PrivateName);
                if (universityName)
                {
                    this.ModelState.AddModelError(nameof(CreateUniversityViewModel.UniversityName), $"Exist {model.UniversityName}");
                    return this.View(model);
                }

                if (!privateName)
                {
                    this.ModelState.AddModelError(nameof(CreateUniversityViewModel.PrivateName), $"Not Found {model.PrivateName}");
                    return this.View(model);
                }

            }

            await this.createUniversityService.CreateAsync(model);
            return this.View();
        }
    }
}
