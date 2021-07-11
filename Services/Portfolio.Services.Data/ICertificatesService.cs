namespace Portfolio.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Portfolio.Web.ViewModels.Administration.Certificate;

    public interface ICertificatesService
    {
        Task CreateAsync(CertificateInputModel model);

        public IEnumerable<T> GetAll<T>(int? count = null);


        public bool FindByName(string name);


        public bool FindById(string id);

        public Task UpdateAsync(EditCertificateInputModel input);
    }
}
