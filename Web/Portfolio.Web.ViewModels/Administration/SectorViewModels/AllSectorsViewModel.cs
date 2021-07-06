using System;
using System.Collections.Generic;
using Portfolio.Web.ViewModels.Administration.PositionViewModel;

namespace Portfolio.Web.ViewModels.Administration.SectorViewModels
{
    public class AllSectorsViewModel
    {
        public IEnumerable<SectorViewModel> Sector { get; set; }
    }
}
