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
        private readonly IDeletableEntityRepository<University> universRepository;
        private readonly IDeletableEntityRepository<Course> courseRepository;

        public CreateCourseService(IDeletableEntityRepository<Certificate>certificateRepository,
            IDeletableEntityRepository<University> universRepository,
            IDeletableEntityRepository<Course>courseRepository)
        {
            this.certificateRepository = certificateRepository;
            this.universRepository = universRepository;
            this.courseRepository = courseRepository;
        }

        public async Task CreateAsync(string courseName, string courseDescription, string date)
        {
            var certificates = this.certificateRepository.All().Where(x => x.Id != null).Select(x => x.Id).ToList();
            var certificateId = certificates[0];

            var universities = this.universRepository.All().Where(x => x.Id != null).Select(x => x.Id).ToList();
            var universityId = universities[0];

            var course = new Course
            {
                Id = Guid.NewGuid().ToString(),
                CourseName = courseName,
                Description = courseDescription,
                Date = date,
                CertificateId = certificateId,
                //UniversityId = universityId,
            };

            await this.courseRepository.AddAsync(course);
            await this.courseRepository.SaveChangesAsync();
        }
    }
}