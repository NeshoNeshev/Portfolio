namespace Portfolio.Data.Models
{

    using Portfolio.Data.Common.Models;

    public class Position : BaseDeletableModel<string>
    {
        public string PositionName { get; set; }

        public string MoreInformation { get; set; }

        public string OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }
    }
}
