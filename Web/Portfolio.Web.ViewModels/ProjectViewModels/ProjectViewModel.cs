namespace Portfolio.Web.ViewModels.ProjectViewModels
{
    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;

    public class ProjectViewModel : IMapFrom<Project>
    {
        public string ProjectName { get; set; }

        public string ImgUrl { get; set; }

        public string SiteUrl { get; set; }

        public string Content { get; set; }
    }
}
