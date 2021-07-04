namespace Portfolio.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;

    public class CreateCourseService : ICreateCourseService
    {
        private readonly IDeletableEntityRepository<Certificate> certificateRepository;
        private readonly IDeletableEntityRepository<Specialty> specialityRepository;
        private readonly IDeletableEntityRepository<Course> courseRepository;
        private readonly ICreateCertificatesService certificatesService;

        public CreateCourseService(IDeletableEntityRepository<Certificate> certificateRepository,
            IDeletableEntityRepository<Specialty>specialityRepository,
            IDeletableEntityRepository<Course> courseRepository,
            ICreateCertificatesService certificatesService)
        {
            this.certificateRepository = certificateRepository;
            this.specialityRepository = specialityRepository;
            this.courseRepository = courseRepository;
            this.certificatesService = certificatesService;
        }


        public async Task CreateAsync(string courseName, string courseDescription, string date, string certificateName, string certificateDate, string certificateDescription, string certificateLInk, string specialityName)
        {
            string item = "default";

            var certificate = this.certificateRepository.All()
                .FirstOrDefault(x => x.CertificateName == certificateName);

            var specialty = this.specialityRepository.All().FirstOrDefault(x => x.SpecialtyName == specialityName);
            if (specialty == null)
            {
                return;
            }

            var course = new Course
            {
                Id = Guid.NewGuid().ToString(),
                CourseName = courseName,
                Description = courseDescription,
                Date = date,
                Specialty = specialty,
            };
            await this.courseRepository.AddAsync(course);
            await this.courseRepository.SaveChangesAsync();
            if (course.Certificates.Count > 0)
            {
                return;
            }

            if (certificate == null)
            {
                await this.certificatesService.CreateAsync(certificateName, certificateLInk, certificateDescription, certificateDate, courseName);
            }
        }
    }
}
