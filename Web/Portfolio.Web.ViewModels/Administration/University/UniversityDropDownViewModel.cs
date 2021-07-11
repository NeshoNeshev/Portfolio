namespace Portfolio.Web.ViewModels.Administration.University
{
    using Portfolio.Services.Mapping;

    public class UniversityDropDownViewModel : IMapFrom<Data.Models.University>
    {
        public string Id { get; set; }

        public string UniversityName { get; set; }
    }
}
