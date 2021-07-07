using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Portfolio.Data.Models;
using Portfolio.Services.Mapping;

namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    public class EditSectorInputModel : IMapFrom<Sector>
    {

        public string Id { get; set; }

        [Required]
        [DisplayName("New Sector Name")]
        public string NewSectorName { get; set; }

        public ICollection<SectorDropDownViewModel> SectorDropDown { get; set; }
    }
}
