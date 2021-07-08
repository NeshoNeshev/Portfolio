using System;
using System.Collections.Generic;
using System.Text;
using Portfolio.Data.Models;
using Portfolio.Services.Mapping;

namespace Portfolio.Web.ViewModels.ProfessionalExperienceViewModels
{
    public class ProfessionalExperienceViewModel : IMapFrom<Organization>
    {
        public string OrganizationName { get; set; }

        public string CompanySize { get; set; }

        public string CountryCountryName { get; set; }

        public ICollection<SectorViewModel> Sectors { get; set; }
    }

    public class SectorViewModel : IMapFrom<Sector>
    {
        public string SectorName { get; set; }

        public ICollection<PositionViewModel> Positions { get; set; }
    }

    public class PositionViewModel : IMapFrom<Position>
    {
        public string PositionName { get; set; }

        public string MoreInformation { get; set; }

        public string Period { get; set; }
    }
}
