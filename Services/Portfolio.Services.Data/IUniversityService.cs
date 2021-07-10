namespace Portfolio.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Portfolio.Web.ViewModels.Administration.Dashboard;
    using Portfolio.Web.ViewModels.Administration.University;

    public interface IUniversityService
    {
        Task CreateAsync(CreateUniversityInputModel model);

        public Task UpdateAsync(EditUniversityInputModel input);

        public IEnumerable<T> GetAll<T>(int? count = null);

        public bool FindByNameAsync(string name);

        public bool FindByIdAsync(string id);
    }
}
