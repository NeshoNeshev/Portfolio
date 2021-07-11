namespace Portfolio.Web.ViewModels.EducationViewModels
{
    using System.Collections.Generic;

    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;

    public class EducationViewModel : IMapFrom<University>
    {
        public string UniversityName { get; set; }

        public string Period { get; set; }

        public ICollection<SpecialityViewModel> Specialties { get; set; }
    }

    public class SpecialityViewModel : IMapFrom<Specialty>
    {
        public string SpecialtyName { get; set; }

        public string Degree { get; set; }

        public ICollection<CourseViewModels> Courses { get; set; }

    }

    public class CourseViewModels: IMapFrom<Course>
    {
        public string CourseName { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public ICollection<CertificateViewModelsz> Certificates { get; set; }
    }

    public class CertificateViewModelsz : IMapFrom<Certificate>
    {
        public string Link { get; set; }

    }
}