using System.Collections;
using System.Collections.Generic;

namespace Portfolio.Data.Models
{
    using Portfolio.Data.Common.Models;

    public class Specialty : BaseDeletableModel<string>
    {
        public Specialty()
        {
            this.Courses = new HashSet<Course>();
        }
        public string SpecialtyName { get; set; }

        public string Degree { get; set; }

        public string UniversityId { get; set; }

        public University University { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
