namespace Portfolio.Web.ViewModels.CourseViewModels
{
    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;

    public class CourseViewModel : IMapFrom<Course>
    {
        public string CourseName { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public string SpecialtySpecialtyName { get; set; }
    }
}
