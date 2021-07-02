namespace Portfolio.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Portfolio.Data.Common.Models;

    public class Course : BaseDeletableModel<string>
    {
        public string CourseName { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public string CertificateId { get; set; }

        public virtual Certificate Certificate { get; set; }

        public string SpecialtyId { get; set; }

        public Specialty Specialty { get; set; }
    }
}
