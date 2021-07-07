using Portfolio.Services.Mapping;

namespace Portfolio.Web.ViewModels.Administration.University
{
    public class UniversityDropDownViewModel : IMapFrom<Data.Models.University>
    {
        public string Id { get; set; }
        public string UniversityName { get; set; }
    }
}