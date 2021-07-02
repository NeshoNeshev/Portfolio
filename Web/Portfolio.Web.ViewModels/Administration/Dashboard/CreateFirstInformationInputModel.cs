namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    using System.ComponentModel.DataAnnotations;

    public class CreateFirstInformationInputModel
    {
        [Required]
        [Display(Name = "Enter the Period")]
        public string Period { get; set; }

        [Required]
        [Display(Name = "Enter the Organization")]
        public string OrganizationName { get; set; }

        [Required]
        [Display(Name = "Enter the Company Size")]
        public string CompanySize { get; set; }

        [Required]
        [Display(Name = "Enter the Company Sector")]
        public string SectorName { get; set; }

        [Required]
        [Display(Name = "Enter the Position Name")]
        public string PositionName { get; set; }

        [Required]
        [Display(Name = "Enter More Information")]
        public string MoreInformation { get; set; }

        [Required]
        [Display(Name = "Enter Country Name")]
        public string CountryName { get; set; }

        [Required]
        [Display(Name = "Enter Town Name")]
        public string TownName { get; set; }

        [Required]
        [Display(Name = "Enter University Name")]
        public string UniversityName { get; set; }

        [Required]
        [Display(Name = "Enter Period")]
        public string UniversityPeriod { get; set; }

        [Required]
        [Display(Name = "Enter Speciality Name")]
        public string SpecialityName { get; set; }

        [Required]
        [Display(Name = "Enter Sepeciality Degree")]
        public string Degree { get; set; }

        [Required]
        [Display(Name = "Enter Certificate Name")]
        public string CertificateName { get; set; }

        [Required]
        [Display(Name = "Enter Certificate Link")]
        public string Link { get; set; }

        [Required]
        [Display(Name = "Enter Certificate Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Enter Certificate Date")]
        public string Date { get; set; }

        [Required]
        [Display(Name = "Enter Course Name")]
        public string CourseName { get; set; }

        [Required]
        [Display(Name = "Enter Course Description")]
        public string CourseDescription { get; set; }

        [Required]
        [Display(Name = "Enter Course Date")]
        public string CourseDate { get; set; }
    }
}