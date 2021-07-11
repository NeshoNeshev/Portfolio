namespace Portfolio.Web.ViewModels.Administration.Specialty
{
    using Portfolio.Services.Mapping;

    public class SpecialtyViewModel : IMapFrom<Data.Models.Specialty>
    {
        public string SpecialtyName { get; set; }

        public string Degree { get; set; }

        public string UniversityUniversityName { get; set; }
    }
}
