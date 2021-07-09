namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    using System.ComponentModel.DataAnnotations;

    public class OrganizationInputModel
    {

        [Required]
        [Display(Name = "Company Name")]
        public string OrganizationName { get; set; }

        [Required]
        [Display(Name = "Organization Size")]
        public string OrganizationSize { get; set; }

        [Required]
        [Display(Name = "Private Name")]
        public string PrivateName { get; set; }

        [Required]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        [Required]
        [Display(Name = "Town Name")]
        public string TownName { get; set; }
    }
}
