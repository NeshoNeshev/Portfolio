namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;

    public class SectorDropDownViewModel : IMapFrom<Sector>
    {
        public string Id { get; set; }

        public string SectorName { get; set; }
    }
}