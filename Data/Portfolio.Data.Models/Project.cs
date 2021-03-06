namespace Portfolio.Data.Models
{
    using Portfolio.Data.Common.Models;

    public class Project : BaseDeletableModel<string>
    {
        public string ProjectName { get; set; }

        public string ImgUrl { get; set; }

        public string SiteUrl { get; set; }

        public string Content { get; set; }

        public PrivateInformation PrivateInformation { get; set; }
    }
}
