using Portfolio.Data.Models;

namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    using System.ComponentModel.DataAnnotations;

    public class OrganizationInputModel
    {

        [Required]
        [Display(Name = "Enter the Company Name")]
        [RegularExpression("^[A-Z][a-z]*$")]
        public string OrganizationName { get; set; }

        [Required]
        [Display(Name = "Enter the Organization Size")]
        public string OrganizationSize { get; set; }

        [Required]
        [Display(Name = "Private Name")]
        [RegularExpression("^[A-Z][a-z]*$")]
        public string PrivateName { get; set; }

        [Required]
        [Display(Name = "Enter the Country Name")]
        [RegularExpression("^[A-Z][a-z]*$")]
        public string CountryName { get; set; }

        [Required]
        [Display(Name = "Enter the Town Name")]
        [RegularExpression("^[A-Z][a-z]*$")]
        public string TownName { get; set; }

        [Required]
        [Display(Name = "Enter the Sector Name")]
        [RegularExpression("^[A-Z][a-z]*$")]
        public string SectorName { get; set; }

        [Required]
        [Display(Name = "Enter the Position Name")]
        [RegularExpression("^[A-Z][a-z]*$")]
        public string PositionName { get; set; }

        [Required]
        [Display(Name = "Enter the Position More Information")]
        public string PositionMoreInformation { get; set; }

        [Required]
        [Display(Name = "Enter the Position Period")]
        public string PositionPeriod { get; set; }

    }
}
