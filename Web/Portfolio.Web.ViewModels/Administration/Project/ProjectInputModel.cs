namespace Portfolio.Web.ViewModels.Administration.Project
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class ProjectInputModel
    {
        [Required]
        [Display(Name = "Enter Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "Enter Image Url")]
        public string ImgUrl { get; set; }

        [Required]
        [DisplayName("Enter site Url")]
        [Url]
        public string SiteUrl { get; set; }

        [Required]
        [DisplayName("Enter Content")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Private Name")]
        public string PrivateName { get; set; }
    }
}
