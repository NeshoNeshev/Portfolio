using System.Threading.Tasks;

namespace Portfolio.Services.Data
{
    public interface ICreateCourseService
    {
        

        Task CreateAsync(string courseName, string courseDescription, string date, string certificateName, string certificateDate, string certificateDescription, string certificateLInk, string specialityName);
    }
}
