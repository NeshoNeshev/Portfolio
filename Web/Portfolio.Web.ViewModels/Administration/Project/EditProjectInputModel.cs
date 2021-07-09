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
        public string NewProjectUrl{ get; set; }

        public ICollection<ProjectDropDownViewModel> ProjectDropDown { get; set; }
    }
}
