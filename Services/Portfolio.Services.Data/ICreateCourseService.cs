using System.Threading.Tasks;

namespace Portfolio.Services.Data
{
    public interface ICreateCourseService
    {
        Task CreateAsync( string courseName, string courseDescription , string date);
    }
}
