using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Portfolio.Services.Mapping;


namespace Portfolio.Web.ViewModels.Administration.University
{
    public class EditUniversityInputModel :IMapFrom<Data.Models.University>
    {
        public string Id { get; set; }

        [Required]
        [DisplayName("New University Name")]
        public string NewUniversityName { get; set; }

        public ICollection<UniversityDropDownViewModel> UniversityDropDown { get; set; }
    }
}
