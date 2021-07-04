using Portfolio.Services.Mapping;

namespace Portfolio.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Portfolio.Data.Models;

    public class CreateSectorViewModel
    {
        [Required]
        public string SectorName { get; set; }

        [Required]
        public string OrganizationName { get; set; }

        [Required]
        public string PositionName { get; set; }

        [Required]
        public string PositionMoreInformation { get; set; }

        [Required]
        public string PositionPeriod { get; set; }

        public IEnumerable<OrganizationDropDownModel> OrganizationDrop { get; set; }
    }

    public class OrganizationDropDownModel:IMapFrom<Organization>
    {
        public string Id { get; set; }
        public string OrganizationName { get; set; }
    }
}
