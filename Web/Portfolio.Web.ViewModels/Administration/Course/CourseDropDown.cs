namespace Portfolio.Web.ViewModels.Administration.Course
{
    using Portfolio.Services.Mapping;

    public class CourseDropDown : IMapFrom<Data.Models.Course>
    {
        public string Id { get; set; }

        public string CourseName { get; set; }
    }
}
