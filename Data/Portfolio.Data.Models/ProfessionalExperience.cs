namespace Portfolio.Data.Models
{
    using System.Collections.Generic;

    using Portfolio.Data.Common.Models;

    public class ProfessionalExperience : BaseDeletableModel<string>
    {
        public ProfessionalExperience()
        {
            this.Organizations = new HashSet<Organization>();
        }
        
        public string Period { get; set; }

        public string PrivateInformationId { get; set; }

        public PrivateInformation PrivateInformation { get; set; }

        public virtual ICollection<Organization> Organizations { get; set; }
    }
}
