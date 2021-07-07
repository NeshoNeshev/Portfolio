using Portfolio.Data.Models;
using Portfolio.Services.Mapping;

namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    public class PositionDropDownViewModel : IMapFrom<Position>
    {
        public string Id { get; set; }
        public string PositionName { get; set; }
    }
}