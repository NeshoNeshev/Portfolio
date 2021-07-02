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

        public CreateCertificatesService(IDeletableEntityRepository<Certificate> certificateRepository)
        {
            this.certificateRepository = certificateRepository;
        }

        public async Task CreateAsync(string certificateName, string link, string description, string date)
        {
            var exist = this.certificateRepository.All().Any(x => x.CertificateName == certificateName);
            if (exist)
            {
                return;
            }

            var certificate = new Certificate
            {
                Id = Guid.NewGuid().ToString(),
                CertificateName = certificateName,
                Link = link,
                Description = description,
                Date = date,
            };

            await this.certificateRepository.AddAsync(certificate);
            await this.certificateRepository.SaveChangesAsync();
        }
    }
}