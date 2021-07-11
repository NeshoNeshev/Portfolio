namespace Portfolio.Web.ViewModels.Administration.PositionViewModel
{
    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;

    public class PositionViewModel : IMapFrom<Position>
    {
        public string PositionName { get; set; }

        public string MoreInformation { get; set; }

        public string Period { get; set; }

        public string SectorSectorName { get; set; }
    }
}