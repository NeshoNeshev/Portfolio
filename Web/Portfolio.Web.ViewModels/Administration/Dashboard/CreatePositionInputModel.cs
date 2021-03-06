namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;

    public class CreatePositionInputModel : IMapFrom<Position>
    {
        [Required]
        [DisplayName("Sectors")]
        public string SectorId { get; set; }

        [Required]
        [Display(Name = "Position Name")]
        public string PositionName { get; set; }

        [Required]
        [Display(Name = "More Information")]
        public string MoreInformation { get; set; }

        [Required]
        [Display(Name = "Position Period")]
        public string PositionPeriod { get; set; }

        public ICollection<SectorDropDownViewModel> SectorDropDown { get; set; }
    }
}
