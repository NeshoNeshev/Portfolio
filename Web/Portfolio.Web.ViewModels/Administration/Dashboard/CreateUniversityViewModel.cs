namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    using System.ComponentModel.DataAnnotations;

    public class CreateUniversityViewModel
    {
        [Required]
        [Display(Name = "Enter the University Name")]
        public string UniversityName { get; set; }

        [Required]
        [Display(Name = "Enter the Period In University")]
        public string PeriodInUniversity { get; set; }

        [Required]
        [Display(Name = "Enter the Period In University")]
        public string CountryName { get; set; }

        [Required]
        [Display(Name = "Private Name")]
        public string PrivateName { get; set; }

        [Required]
        [Display(Name = "Town Name")]
        public string TownName { get; set; }

        [Required]
        [Display(Name = "Speciality Name")]
        public string SpecialityName { get; set; }

        [Required]
        [Display(Name = "Speciality Degree")]
        public string SpecialityDegree { get; set; }

        [Required]
        [Display(Name = "Course Degree")]
        public string CourseName { get; set; }

        [Required]
        [Display(Name = "Course Description")]
        public string CourseDescription { get; set; }

        [Required]
        [Display(Name = "Course Date")]
        public string CourseDate{ get; set; }

        [Required]
        [Display(Name = "Certificate Name")]
        public string CertificateName { get; set; }

        [Required]
        [Display(Name = "Certificate Description")]
        public string CertificateDescription { get; set; }

        [Required]
        [Display(Name = "Certificate Date")]
        public string CertificateDate { get; set; }

        [Required]
        [Display(Name = "Certificate Link")]
        [Url]
        public string CertificateLink{ get; set; }
    }
}
