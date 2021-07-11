namespace Portfolio.Web.ViewModels.Administration.PositionViewModel
{
    using System.Collections.Generic;

    using Portfolio.Web.ViewModels.Administration.Dashboard;

    public class AllEditPositionModel
    {
        public IEnumerable<EditPositionViewModel> Positions { get; set; }
    }
}
