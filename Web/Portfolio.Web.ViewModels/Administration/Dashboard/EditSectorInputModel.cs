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

        [Required]
        [DisplayName("Sector Name")]
        public string SectorName { get; set; }

        [Required]
        public string NewSectorName { get; set; }
    }
}
