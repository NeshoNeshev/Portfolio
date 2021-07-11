namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;

    public class PositionDropDownViewModel : IMapFrom<Position>
    {
        public string Id { get; set; }

        public string PositionName { get; set; }
    }
}
