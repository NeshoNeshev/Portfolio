namespace Portfolio.Web.ViewModels.OrganizationViewModels
{
    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;

    public class OrganizationViewModel : IMapFrom<Organization>
    {
        public string OrganizationName { get; set; }

        public string CompanySize { get; set; }

        public string CountryCountryName { get; set; }

        public string PrivateInformationFirstName { get; set; }
    }
}
