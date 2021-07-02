using System.Threading.Tasks;

namespace Portfolio.Services.Data
{
    public interface ICreateUniversityService
    {
        Task CreateAsync(string name, string period);
    }
}
