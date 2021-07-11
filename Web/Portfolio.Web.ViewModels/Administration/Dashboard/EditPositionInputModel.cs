namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

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

        public ICollection<PositionDropDownViewModel> DropDownModel { get; set; }
    }
}
