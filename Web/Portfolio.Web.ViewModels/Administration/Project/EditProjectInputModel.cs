using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Portfolio.Web.Areas.Administration.Views.Organization;

namespace Portfolio.Web.ViewModels.Administration.Project
{
    public class EditProjectInputModel
    {
        public string Id { get; set; }

        [Required]
        [DisplayName("New Project Name")]
        public string NewProjectName { get; set; }

        [Required]
        [DisplayName("New Project Url")]
        public string NewProjectUrl{ get; set; }

        public ICollection<ProjectDropDownViewModel> ProjectDropDown { get; set; }
    }
}
