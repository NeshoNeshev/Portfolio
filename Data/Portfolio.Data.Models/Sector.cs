namespace Portfolio.Data.Models
{
    using System.Collections.Generic;

    using Portfolio.Data.Common.Models;

    public class Sector : BaseDeletableModel<string>
    {
        public Sector()
        {
            this.Positions = new HashSet<Position>();
        }

        public string SectorName { get; set; }

        public string OrganizationId { get; set; }

        public Organization Organization { get; set; }

        public virtual ICollection<Position> Positions { get; set; }
    }
}
