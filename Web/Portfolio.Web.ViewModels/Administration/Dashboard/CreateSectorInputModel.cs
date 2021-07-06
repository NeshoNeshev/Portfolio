namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;


    public class CreateSectorInputModel
    {
        [Required]
        public string SectorName { get; set; }

        [Required]
        [DisplayName("Organization Name")]
        public string OrganizationName { get; set; }

    }
}
