namespace Portfolio.Data.Models
{
    using Portfolio.Data.Common.Models;

    public class Certificate : BaseDeletableModel<string>
    {
        public string CertificateName { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public string CourseId { get; set; }

        public Course Course { get; set; }
    }
}
