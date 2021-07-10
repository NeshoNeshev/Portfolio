namespace Portfolio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;
    using Portfolio.Web.ViewModels.Administration.Certificate;

    public class CertificatesService : ICertificatesService
    {
        private readonly IDeletableEntityRepository<Certificate> certificateRepository;
        private readonly IDeletableEntityRepository<Course> courseRepository;

        public CertificatesService(IDeletableEntityRepository<Certificate> certificateRepository,IDeletableEntityRepository<Course>courseRepository)
        {
            this.certificateRepository = certificateRepository;
            this.courseRepository = courseRepository;
        }

        public async Task CreateAsync(CertificateInputModel model)
        {
            var course = this.courseRepository.All().FirstOrDefault(x => x.Id == model.CourseId);
            if (course == null)
            {
                return;
            }

            var certificate = new Certificate
            {
                Id = Guid.NewGuid().ToString(),
                CertificateName = model.CertificateName,
                Link = model.Link,
                Description = model.Description,
                Date = model.Date,
            };
            certificate.Course = course;
            await this.certificateRepository.AddAsync(certificate);
            await this.certificateRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Certificate> query = this.certificateRepository.All().OrderBy(x => x.CertificateName);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public bool FindByNameAsync(string name)
            => this.certificateRepository
                .All()
                .Any(s => s.CertificateName == name);

        public bool FindByIdAsync(string id) => this.certificateRepository
            .All()
            .Any(x => x.Id == id);

        public async Task UpdateAsync(EditCertificateInputModel input)
        {
            var certificate = this.certificateRepository
                .All()
                .FirstOrDefault(x => x.Id == input.Id);
            if (certificate != null)
            {
                certificate.CertificateName = input.NewCertificateName;
                certificate.Description = input.NewDescription;
                certificate.Date = input.NewDate;
                certificate.Link = input.NewLink;
                this.certificateRepository.Update(certificate);
            }

            await this.certificateRepository.SaveChangesAsync();
        }
    }
}