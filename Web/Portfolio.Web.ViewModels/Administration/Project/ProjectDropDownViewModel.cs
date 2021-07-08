using System;
using System.Collections.Generic;
using System.Text;
using Portfolio.Services.Mapping;

namespace Portfolio.Web.ViewModels.Administration.Project
{
    public class ProjectDropDownViewModel : IMapFrom<Data.Models.Project>
    {
        public string Id { get; set; }

        public string ProjectName { get; set; }
    }
}
