namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;

    public class EditSectorInputModel : IMapFrom<Sector>
    {
        [Required]
        [DisplayName("Sector")]
        public string Id { get; set; }

        [Required]
        [DisplayName("New Sector Name")]
        public string NewSectorName { get; set; }

        public ICollection<SectorDropDownViewModel> SectorDropDown { get; set; }
    }
}
