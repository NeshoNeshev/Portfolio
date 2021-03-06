namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;

    public class EditPositionViewModel : IMapFrom<Position>
    {
        public string Id { get; set; }

        public string PositionName { get; set; }

        public string MoreInformation { get; set; }

        public string Period { get; set; }
    }
}
