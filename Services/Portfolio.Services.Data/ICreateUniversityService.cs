namespace Portfolio.Services.Data
{
    using System.Threading.Tasks;

    using Portfolio.Web.ViewModels.Administration.Dashboard;

    public interface ICreateUniversityService
    {
        Task CreateAsync(CreateUniversityInputModel model);
    }
}
