namespace Portfolio.Web.ViewModels.Administration.Course
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class EditCourseInputModel
    {
        public string Id { get; set; }

        [Required]
        [DisplayName("New Course Name")]
        public string NewCourseName { get; set; }

        [Required]
        [DisplayName("New Description")]
        public string NewDescription { get; set; }

        [Required]
        [DisplayName("New Date")]
        public string Date { get; set; }

        public ICollection<CourseDropDown> CourseDropDowns { get; set; }
    }
}
