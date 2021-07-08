using System;
using System.Collections.Generic;
using System.Text;
using Portfolio.Data.Common.Models;

namespace Portfolio.Data.Models
{
    public class Project : BaseDeletableModel<string>
    {
        public string ProjectName { get; set; }

        public string ImgUrl { get; set; }

        public PrivateInformation PrivateInformation { get; set; }
    }
}
