namespace Portfolio.Data.Models
{
    using Portfolio.Data.Common.Models;

    public class Specialty : BaseDeletableModel<string>
    {
        public string SpecialtyName { get; set; }

        public string Degree { get; set; }

        public string UniversityId { get; set; }

        public University University { get; set; }
    }
}
