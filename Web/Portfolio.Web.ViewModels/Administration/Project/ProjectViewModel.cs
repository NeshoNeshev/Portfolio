namespace Portfolio.Web.ViewModels.Administration.Project
{
    using Portfolio.Services.Mapping;

    public class ProjectViewModel : IMapFrom<Data.Models.Project>
    {
        public string ProjectName { get; set; }

        public string ImgUrl { get; set; }

        public string SiteUrl { get; set; }

        public string Content { get; set; }

        public string PrivateInformationFirstName { get; set; }
    }
}
