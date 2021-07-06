namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;

    public class CreatePositionInputModel
    {
        [Required]
        [DisplayName("Organization Name")]
        public string OrganizationId { get; set; }

        [Required]
        [DisplayName("Sector Name")]
        public string SectorId { get; set; }

        [Required]
        [Display(Name = "Enter the Position Name")]
        [RegularExpression("^[A-Z][a-z]*$")]
        public string PositionName { get; set; }

        [Required]
        [Display(Name = "Enter More Information")]
        public string MoreInformation { get; set; }

        [Required]
        [Display(Name = "Enter Position Period")]
        public string PositionPeriod { get; set; }
    }
}
