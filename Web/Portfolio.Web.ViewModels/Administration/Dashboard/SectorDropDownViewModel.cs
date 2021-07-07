using Portfolio.Data.Models;
using Portfolio.Services.Mapping;

namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    public class SectorDropDownViewModel : IMapFrom<Sector>
    {
        public string Id { get; set; }

        public string SectorName { get; set; }
    }
}