using Portfolio.Data.Models;
using Portfolio.Services.Mapping;

namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    public class OrganizationDropDownViewModel : IMapFrom<Organization>
    {
        public string Id { get; set; }
        public string OrganizationName { get; set; }
    }
}