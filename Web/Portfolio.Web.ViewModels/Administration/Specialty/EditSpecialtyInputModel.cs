namespace Portfolio.Web.ViewModels.Administration.Specialty
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;

    public class EditSpecialtyInputModel : IMapFrom<Specialty>
    {
        [DisplayName("Specialty")]
        public string Id { get; set; }

        [Required]
        [DisplayName("New Specialty Name")]
        public string NewSpecialtyName { get; set; }

        public string Degree { get; set; }

        [Required]
        [DisplayName("NewDegree")]
        public string NewDegree { get; set; }

        public ICollection<SpecialtyDropDown> SpecialtyDropDowns { get; set; }
    }
}
