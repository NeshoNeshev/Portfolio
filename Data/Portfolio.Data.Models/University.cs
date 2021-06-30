namespace Portfolio.Data.Models
{
    using System.Collections.Generic;

    using Portfolio.Data.Common.Models;

    public class University : BaseDeletableModel<string>
    {
        public University()
        {
            this.Courses = new HashSet<Course>();
        }

        public string UniversityName { get; set; }

        public string Period { get; set; }

        public string CountryId { get; set; }

        public virtual Country Country { get; set; }

        public string PrivateInformationId { get; set; }

        public PrivateInformation PrivateInformation { get; set; }

        public virtual ICollection<Specialty> Specialties { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
