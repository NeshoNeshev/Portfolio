namespace Portfolio.Data.Models
{
    using System.Collections.Generic;

    using Portfolio.Data.Common.Models;

    public class PrivateInformation : BaseDeletableModel<string>
    {

        public PrivateInformation()
        {
            this.Organization = new HashSet<Organization>();
            this.Universities = new HashSet<University>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ThirdName { get; set; }

        public string Birthday { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        public virtual ICollection<Organization> Organization { get; set; }

        public virtual ICollection<University> Universities { get; set; }
    }
}
