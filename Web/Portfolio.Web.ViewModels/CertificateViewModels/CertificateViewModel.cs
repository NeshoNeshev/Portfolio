namespace Portfolio.Web.ViewModels.CertificateViewModels
{
    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;

    public class CertificateViewModel : IMapFrom<Certificate>
    {
        public string CourseCourseName { get; set; }

        public string CertificateName { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }
    }
}
