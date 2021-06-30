namespace Portfolio.Data.Models
{
    using System.Collections.Generic;

    using Portfolio.Data.Common.Models;

    public class Organization : BaseDeletableModel<string>
    {
        public Organization()
        {
            this.Positions = new HashSet<Position>();
            this.Sectors = new HashSet<Sector>();
        }

        public string OrganizationName { get; set; }

        public string CompanySize { get; set; }

        public virtual ICollection<Position> Positions { get; set; }

        public string CountryId { get; set; }

        public Country Country { get; set; }

        public string ProfessionalExperienceId { get; set; }

        public ProfessionalExperience ProfessionalExperience { get; set; }

        public virtual ICollection<Sector> Sectors { get; set; }
    }
}
