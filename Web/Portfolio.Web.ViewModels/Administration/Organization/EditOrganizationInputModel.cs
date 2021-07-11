namespace Portfolio.Web.ViewModels.Administration.Organization
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Portfolio.Services.Mapping;

    public class EditOrganizationInputModel : IMapFrom<Data.Models.Organization>
    {
        [DisplayName("Organization")]
        public string Id { get; set; }

        [Required]
        [DisplayName("New Organization Name")]
        public string NewOrganizationName { get; set; }

        public string CompanySize { get; set; }

        [Required]
        [DisplayName("New Company Size")]
        public string NewCompanySize { get; set; }

        public ICollection<OrganizationDropDownViewModel> OrganizaztionDropDown { get; set; }
    }
}
