namespace Portfolio.Web.ViewModels.Administration.Project
{
    using System.ComponentModel.DataAnnotations;

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
        public string PrivateName { get; set; }
    }
}
