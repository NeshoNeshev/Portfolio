using System.Threading.Tasks;

namespace Portfolio.Services.Data
{
    public interface ICreateSpecialtiesService
    {
        Task CreateAsync(string specialtyName, string degree, string universityName);
    }
}
