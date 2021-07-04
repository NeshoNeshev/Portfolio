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
        private readonly IDeletableEntityRepository<Course> _c;

        public CreateCertificatesService(IDeletableEntityRepository<Certificate> certificateRepository,IDeletableEntityRepository<Course>c)
        {
            this.certificateRepository = certificateRepository;
            _c = c;
        }

        public async Task CreateAsync(string certificateName, string link, string description, string date, string cName)
        {
            var exist = this.certificateRepository.All().Any(x => x.CertificateName == certificateName);
            if (exist)
            {
                return;
            }

            var c = this._c.All().FirstOrDefault(x => x.CourseName == cName);
            var certificate = new Certificate
            {
                Id = Guid.NewGuid().ToString(),
                CertificateName = certificateName,
                Link = link,
                Description = description,
                Date = date,
            };
            certificate.Course = c;
            await this.certificateRepository.AddAsync(certificate);
            await this.certificateRepository.SaveChangesAsync();
        }
    }
}