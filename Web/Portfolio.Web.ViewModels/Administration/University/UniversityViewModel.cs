namespace Portfolio.Web.ViewModels.Administration.University
{
    using Portfolio.Services.Mapping;

    public class UniversityViewModel : IMapFrom<Data.Models.University>
    {
        public string UniversityName { get; set; }

        public string Period { get; set; }

        public string CountryCountryName { get; set; }

        public string PrivateInformationFirstName { get; set; }
    }
}
