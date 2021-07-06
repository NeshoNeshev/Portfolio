
using Portfolio.Data.Models;
using Portfolio.Services.Mapping;

namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    public class EditPositionViewModel : IMapFrom<Position>
    {
        public string SectorId { get; set; }

        public string PositionName { get; set; }

        public string MoreInformation { get; set; }

        public string Period { get; set; }
    }
}
