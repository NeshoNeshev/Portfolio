using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Portfolio.Web.ViewModels.Administration.Project
{
    public class ProjectInputModel
    {
        [Required]
        [Display(Name = "Enter Project Name")]
        public string ProjectName { get; set; }

        [Required]
        [Display(Name = "Enter Image Url")]
        public string ImgUrl { get; set; }

        [Required]
        [Display(Name = "Private Name")]
        [RegularExpression("^[A-Z][a-z]*$")]
        public string PrivateName { get; set; }
    }
}
