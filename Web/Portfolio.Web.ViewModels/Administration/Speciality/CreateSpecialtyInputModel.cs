namespace Portfolio.Web.ViewModels.Administration.Speciality
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;
    using Portfolio.Web.ViewModels.Administration.University;

    public class CreateSpecialtyInputModel : IMapFrom<Specialty>
    {

        [Required]
        [DisplayName("Specialty Name")]
        public string SpecialtyName { get; set; }

        [Required]
        [DisplayName("Specialty Degree")]
        public string Degree { get; set; }

        [Required]
        [DisplayName("University")]
        public string UniversityId { get; set; }

        public ICollection<UniversityDropDownViewModel> UniversityDropDown { get; set; }
    }

}
