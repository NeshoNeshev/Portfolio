using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Portfolio.Data.Models;
using Portfolio.Services.Mapping;

namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    public class EditPositionInputModel : IMapFrom<Position>
    {
        [Required]
        [DisplayName("Position Name")]
        public string PositionName { get; set; }

        [Required]
        [DisplayName("MoreInformation")]
        public string MoreInformation { get; set; }

        [Required]
        [DisplayName("Period")]
        public string Period { get; set; }
    }
}
