using System;
using System.Collections.Generic;
using System.Text;
using Portfolio.Web.ViewModels.Administration.Dashboard;

namespace Portfolio.Web.ViewModels.Administration.PositionViewModel
{
    public class AllPositionModel
    {
        public IEnumerable<EditPositionViewModel> Positions { get; set; }
    }
}
