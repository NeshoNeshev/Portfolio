namespace Portfolio.Web.ViewModels.Administration.SectorViewModels
{
    using System.Collections.Generic;

    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;

    public class SectorViewModel : IMapFrom<Sector>
    {
        public string SectorName { get; set; }

        public  ICollection<PositionViewModel.PositionViewModel> Positions { get; set; }
    }
}
