using Portfolio.Data.Models;
using Portfolio.Services.Mapping;

namespace Portfolio.Web.ViewModels.CertificateViewModels
{
    public class CertificateViewModel : IMapFrom<Certificate>
    {
        public string CertificateName { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

    }
}
