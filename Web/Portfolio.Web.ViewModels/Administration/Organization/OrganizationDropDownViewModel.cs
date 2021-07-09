namespace Portfolio.Web.Areas.Administration.Views.Organization
{
    using Portfolio.Services.Mapping;

    public class OrganizationDropDownViewModel : IMapFrom<Data.Models.Organization>
    {
        public string Id { get; set; }

        public string OrganizationName { get; set; }

    }
}