namespace Portfolio.Data.Models
{
    using System.Collections.Generic;

    using Portfolio.Data.Common.Models;

    public class Country : BaseDeletableModel<string>
    {
        public Country()
        {
            this.Towns = new HashSet<Town>();
            this.Organizations = new HashSet<Organization>();
        }

        public string CountryName { get; set; }

        public virtual ICollection<Organization> Organizations { get; set; }

        public virtual ICollection<Town> Towns { get; set; }

        public virtual ICollection<University> Universities { get; set; }
    }
}
