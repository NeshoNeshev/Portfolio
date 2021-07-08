namespace Portfolio.Web.ViewModels.Administration.Certificate
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Portfolio.Services.Mapping;
    using Portfolio.Web.ViewModels.Administration.Course;

    public class CertificateInputModel : IMapFrom<Data.Models.Certificate>
    {
        public string Id { get; set; }

        [Required]
        [DisplayName("Certificate Name")]
        public string CertificateName { get; set; }

        [Required]
        [DisplayName("Link")]
        public string Link { get; set; }


        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Date")]
        public string Date { get; set; }

        public string CourseId { get; set; }

        public ICollection<CourseDropDown> CourseDropDowns { get; set; }
    }
}
