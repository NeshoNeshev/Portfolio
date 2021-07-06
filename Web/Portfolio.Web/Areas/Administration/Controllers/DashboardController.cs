using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Portfolio.Common;
using Portfolio.Web.ViewModels.Administration.Dashboard;

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






        //await this.sectorService.CreateAsync(input.SectorName);

        //await this._positionService.CreateAsync(input.PositionName, input.MoreInformation);

        //await this._countryService.CreateAsync(input.CountryName);

        //await this._createTownService.CreateAsync(input.TownName);

        //await this._createUniversityService.CreateAsync(input.UniversityName, input.UniversityPeriod);

        //await this._specialtiesService.CreateAsync(input.SpecialityName, input.Degree);

        //await this._certificatesService.CreateAsync(input.CertificateName, input.Link, input.Description, input.Date);

        //await this._courseService.CreateAsync(input.CourseName, input.CourseDescription, input.CourseDescription);

    }
}
