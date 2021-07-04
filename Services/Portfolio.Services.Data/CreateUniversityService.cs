namespace Portfolio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Web.ViewModels.Administration.Dashboard;

    public class CreateUniversityService : ICreateUniversityService
    {
        private readonly IDeletableEntityRepository<University> universityRepository;
        private readonly IDeletableEntityRepository<Course> _couorsEntityRepository;
        private readonly IDeletableEntityRepository<Specialty> _specialityRepository;
        private readonly IDeletableEntityRepository<Country> countryRepository;
        private readonly IDeletableEntityRepository<PrivateInformation> privatEntityRepository;
        private readonly IChangeInputToUpper<CreateUniversityViewModel> changeInputToUpper;
        private readonly ICreateCountryService createCountry;
        private readonly ICreateTownService townService;
        private readonly ICreateSpecialtiesService _specialtiesService;
        private readonly ICreateCourseService _courseService;

        public CreateUniversityService(IDeletableEntityRepository<University> universityRepository,
            IDeletableEntityRepository<Course> couorsEntityRepository,
            IDeletableEntityRepository<Specialty> specialityRepository,
            IDeletableEntityRepository<Country> countryRepository,
            IDeletableEntityRepository<PrivateInformation> privatEntityRepository,
            IChangeInputToUpper<CreateUniversityViewModel> changeInputToUpper,
            ICreateCountryService createCountry,
            ICreateTownService townService,
            ICreateSpecialtiesService specialtiesService,
            ICreateCourseService courseService)
        {
            this.universityRepository = universityRepository;
            _couorsEntityRepository = couorsEntityRepository;
            _specialityRepository = specialityRepository;
            this.countryRepository = countryRepository;
            this.privatEntityRepository = privatEntityRepository;
            this.changeInputToUpper = changeInputToUpper;
            this.createCountry = createCountry;
            this.townService = townService;
            _specialtiesService = specialtiesService;
            _courseService = courseService;
        }

        public async Task CreateAsync(CreateUniversityViewModel model)
        {
            var privateInformation = this.privatEntityRepository.All().FirstOrDefault(x => x.FirstName == model.PrivateName);
            var universityExist = this.universityRepository.All().Any(x => x.UniversityName == model.UniversityName);
            if (universityExist)
            {
                return;
            }

            if (privateInformation == null)
            {
                return;
            }

            //List<string> forbidden = new List<string>() { model.CourseDescription, model.CertificateDescription, model.CertificateLink };
            //this.changeInputToUpper.ToUpper(model, forbidden);

            var country = this.countryRepository.All().FirstOrDefault(x => x.CountryName == model.CountryName);

            if (country == null)
            {
                await this.createCountry.CreateAsync(model.CountryName, model.TownName);
            }
            else
            {
                var exist = country.Towns.Any(x => x.TownName == model.TownName);
                if (!exist)
                {
                    await this.townService.CreateAsync(model.TownName, model.CountryName);
                }
            }
            var university = new University
            {
                Id = Guid.NewGuid().ToString(),
                UniversityName = model.UniversityName,
                Period = model.PeriodInUniversity,
            };
            university.PrivateInformation = privateInformation;
            university.Country = country;

            await this.universityRepository.AddAsync(university);
            await this.universityRepository.SaveChangesAsync();

            var specialty = this._specialityRepository.All().FirstOrDefault(x => x.SpecialtyName == model.SpecialityName);
            var course = this._couorsEntityRepository.All().FirstOrDefault(x => x.CourseName == model.CourseName);

            if (specialty == null)
            {

                await this._specialtiesService.CreateAsync(model.SpecialityName, model.SpecialityDegree, model.UniversityName);
                var spec = this._specialityRepository.All()
                    .FirstOrDefault(x => x.SpecialtyName == model.SpecialityName);
                if (!spec.Courses.Contains(course))
                {
                    await this._courseService.CreateAsync(model.CourseName, model.CourseDescription, model.CourseDate, model.CertificateName, model.CertificateDate, model.CertificateDescription, model.CertificateLink,model.SpecialityName);
                    spec.Courses.Add(course);
                }
            }
            else
            {
                if (course == null)
                {
                    await this._courseService.CreateAsync(model.CourseName, model.CourseDescription, model.CourseDate, model.CertificateName, model.CertificateDate, model.CertificateDescription, model.CertificateLink, model.SpecialityName);
                }

                specialty.Courses.Add(course);
                university.Specialties.Add(specialty);
            }
        }
    }
}