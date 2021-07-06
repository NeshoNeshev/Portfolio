using System;
using System.Linq;
using System.Threading.Tasks;
using Portfolio.Data.Common.Repositories;
using Portfolio.Data.Models;

namespace Portfolio.Services.Data
{
    public class CreateCertificatesService : ICreateCertificatesService
    {
        private readonly IDeletableEntityRepository<Certificate> certificateRepository;
        private readonly IDeletableEntityRepository<Course> courseRepository;

        public CreateCertificatesService(IDeletableEntityRepository<Certificate> certificateRepository,IDeletableEntityRepository<Course>course)
        {
            this.certificateRepository = certificateRepository;
            course = course;
        }

        public async Task CreateAsync(string certificateName, string link, string description, string date, string cName)
        {
            var exist = this.certificateRepository.All().Any(x => x.CertificateName == certificateName);
            if (exist)
            {
                return;
            }

            var course = this.courseRepository.All().FirstOrDefault(x => x.CourseName == cName);
            var certificate = new Certificate
            {
                Id = Guid.NewGuid().ToString(),
                CertificateName = certificateName,
                Link = link,
                Description = description,
                Date = date,
            };
            certificate.Course = course;
            await this.certificateRepository.AddAsync(certificate);
            await this.certificateRepository.SaveChangesAsync();
        }
    }
}