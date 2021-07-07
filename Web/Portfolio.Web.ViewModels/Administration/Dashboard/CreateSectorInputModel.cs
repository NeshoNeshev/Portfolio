using Portfolio.Data.Models;
using Portfolio.Services.Mapping;

namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class CreateSectorInputModel : IMapFrom<Sector>
    {

        [Required]
        [DisplayName("Sector Name")]
        public string SectorName { get; set; }

        [Required]
        [DisplayName("Organizations")]
        public string OrganizationId { get; set; }

        public ICollection<OrganizationDropDownViewModel> OrganizationDropDown { get; set; }

    }
}
