using Portfolio.Services.Mapping;

namespace Portfolio.Web.Areas.Administration.Views.Organization
{
    public class OrganizationDropDownViewModel : IMapFrom<Data.Models.Organization>
    {
        public string Id { get; set; }
        public string OrganizationName { get; set; }
    }
}