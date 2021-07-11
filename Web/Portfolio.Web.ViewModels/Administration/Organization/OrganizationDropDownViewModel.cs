namespace Portfolio.Web.ViewModels.Administration.Organization
{
    using Portfolio.Services.Mapping;

    public class OrganizationDropDownViewModel : IMapFrom<Data.Models.Organization>
    {
        public string Id { get; set; }

        public string OrganizationName { get; set; }

    }
}
