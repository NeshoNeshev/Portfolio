namespace Portfolio.Web.ViewModels.Administration.Project
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class EditProjectInputModel
    {
        [Required]
        [DisplayName("Project")]
        public string Id { get; set; }

        [Required]
        [DisplayName("New Project Name")]
        public string NewProjectName { get; set; }

        [Required]
        [DisplayName("New Project Url")]
        [Url]
        public string NewImgUrl{ get; set; }

        [DisplayName("Enter new site Url")]
        [Url]
        public string NewSiteUrl { get; set; }

        [Required]
        [DisplayName("Enter Content")]
        public string NewContent { get; set; }

        public ICollection<ProjectDropDownViewModel> ProjectDropDown { get; set; }
    }
}
