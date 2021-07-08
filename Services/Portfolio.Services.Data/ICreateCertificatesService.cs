namespace Portfolio.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Portfolio.Web.ViewModels.Administration.Certificate;

    public interface ICreateCertificatesService
    {
        Task CreateAsync(CertificateInputModel model);

        public IEnumerable<T> GetAll<T>(int? count = null);


        public bool FindByNameAsync(string name);


        public bool FindByIdAsync(string id);

        public Task UpdateAsync(EditCertificateInputModel input);
    }
}
