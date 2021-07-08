using Portfolio.Services.Mapping;

namespace Portfolio.Web.ViewModels.Administration.Certificate
{
    public class CertificateDropDown : IMapFrom<Data.Models.Certificate>
    {
        public string Id { get; set; }

        public string CertificateName { get; set; }
    }
}