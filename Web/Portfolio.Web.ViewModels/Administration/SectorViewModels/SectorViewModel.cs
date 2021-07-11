namespace Portfolio.Web.ViewModels.Administration.SectorViewModels
{
    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;

    public class SectorViewModel : IMapFrom<Sector>
    {
        public string SectorName { get; set; }

        public string OrganizationOrganizationName{ get; set; }
    }
}
