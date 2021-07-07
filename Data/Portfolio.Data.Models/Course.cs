namespace Portfolio.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Portfolio.Data.Common.Models;

    public class Course : BaseDeletableModel<string>
    {
        public Course()
        {
            this.Certificates = new HashSet<Certificate>();
        }

        public string CourseName { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public string SpecialtyId { get; set; }

        public Specialty Specialty { get; set; }

        public virtual ICollection<Certificate> Certificates { get; set; }
    }
}
