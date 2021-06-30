namespace Portfolio.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Portfolio.Data.Common.Models;

    public class Sector : BaseDeletableModel<string>
    {

        public string SectorName { get; set; }

        public string OrganizationId { get; set; }

        public Organization Organization { get; set; }
    }
}
