namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    using System.ComponentModel.DataAnnotations;

    public class CreateUniversityInputModel
    {
        [Required]
        [Display(Name = "Enter the University Name")]
        public string UniversityName { get; set; }

        [Required]
        [Display(Name = "Enter the Period In University")]
        public string PeriodInUniversity { get; set; }

        [Required]
        [Display(Name = "Private Name")]
        public string PrivateName { get; set; }

        [Required]
        [Display(Name = "Enter the Country Name")]
        public string CountryName { get; set; }

        [Required]
        [Display(Name = "Town Name")]
        public string TownName { get; set; }
    }
}
