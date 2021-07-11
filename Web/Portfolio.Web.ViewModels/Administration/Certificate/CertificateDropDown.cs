namespace Portfolio.Web.ViewModels.Administration.Certificate
{
    using Portfolio.Services.Mapping;

    public class CertificateDropDown : IMapFrom<Data.Models.Certificate>
    {
        public string Id { get; set; }

        public string CertificateName { get; set; }
    }
}
