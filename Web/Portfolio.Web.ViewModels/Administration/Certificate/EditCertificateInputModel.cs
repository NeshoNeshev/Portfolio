namespace Portfolio.Web.ViewModels.Administration.Certificate
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class EditCertificateInputModel
    {
        public string Id { get; set; }

        [Required]
        [DisplayName("New Certificate Name")]
        public string NewCertificateName { get; set; }

        [DisplayName("New Description")]
        public string NewDescription { get; set; }

        [Required]
        [DisplayName("New Date")]
        public string NewDate { get; set; }

        [Required]
        [DisplayName("New Link")]
        public string NewLink { get; set; }

        public ICollection<CertificateDropDown> CertificateDropDowns { get; set; }
    }
}
