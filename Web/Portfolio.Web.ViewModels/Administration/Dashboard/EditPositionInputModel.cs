using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Portfolio.Data.Models;
using Portfolio.Services.Mapping;

namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    public class EditPositionInputModel 
    {

        [Required]
        [DisplayName("Positions")]
        public string Id { get; set; }

        [Required]
        [DisplayName("New Position Name")]
        public string NewPositionName { get; set; }

        [Required]
        [DisplayName("MoreInformation")]
        public string MoreInformation { get; set; }

        [Required]
        [DisplayName("Period")]
        public string Period { get; set; }

        [DisplayName("PositionName")]
        public ICollection<PositionDropDownViewModel> DropDownModel { get; set; }
    }
}
