namespace Portfolio.Data.Models
{

    using Portfolio.Data.Common.Models;

    public class Position : BaseDeletableModel<string>
    {
        public string PositionName { get; set; }

        public string MoreInformation { get; set; }

        public string Period { get; set; }

        public string SectorId { get; set; }

        public virtual Sector Sector { get; set; }
    }
}
