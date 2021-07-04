namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    using System.ComponentModel.DataAnnotations;

    public class CreateUniversityViewModel
    {

        [Required]
        [Display(Name = "Enter the University Name")]
        [RegularExpression("^[A-Z][a-z]*$")]
        public string UniversityName { get; set; }

        [Required]
        [Display(Name = "Enter the Period In University")]
        public string PeriodInUniversity { get; set; }

        [Required]
        [Display(Name = "Private Name")]
        public string PrivateName { get; set; }

        [Required]
        [Display(Name = "Enter the Country Name")]
        [RegularExpression("^[A-Z][a-z]*$")]
        public string CountryName { get; set; }

        [Required]
        [Display(Name = "Town Name")]
        [RegularExpression("^[A-Z][a-z]*$")]
        public string TownName { get; set; }

        [Required]
        [Display(Name = "Speciality Name")]
        [RegularExpression("^[A-Z][a-z]*$")]
        public string SpecialityName { get; set; }

        [Required]
        [Display(Name = "Speciality Degree")]
        [RegularExpression("^[A-Z][a-z]*$")]
        public string SpecialityDegree { get; set; }

        [Required]
        [Display(Name = "Course Name")]
        [RegularExpression("^[A-Z][a-z]*$")]
        public string CourseName { get; set; }

        [Required]
        [Display(Name = "Course Description")]
        public string CourseDescription { get; set; }

        [Required]
        [Display(Name = "Course Date")]
        public string CourseDate{ get; set; }

        [Display(Name = "Certificate Name")]
        public string CertificateName { get; set; }

        [Display(Name = "Certificate Description")]
        public string CertificateDescription { get; set; }

        [Display(Name = "Certificate Date")]
        public string CertificateDate { get; set; }

        [Display(Name = "Certificate Link")]
        public string CertificateLink{ get; set; }
    }
}
