namespace Portfolio.Web.ViewModels.Administration.Course
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Portfolio.Services.Mapping;
    using Portfolio.Web.ViewModels.Administration.Specialty;

    public class CourseInputModel : IMapFrom<Data.Models.Course>
    {

        [Required]
        [DisplayName("Course Name")]
        public string CourseName { get; set; }

        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Course Date")]
        public string Date { get; set; }

        [Required]
        [DisplayName("Specialty")]
        public string SpecialtyId { get; set; }

        public ICollection<SpecialtyDropDown> SpecialtiesDropDowns { get; set; }
    }
}
