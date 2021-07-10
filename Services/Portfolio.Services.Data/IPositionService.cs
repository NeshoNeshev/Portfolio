namespace Portfolio.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Portfolio.Web.ViewModels.Administration.Dashboard;

    public interface IPositionService
    {
        Task CreateAsync(CreatePositionInputModel model);

        public Task UpdateAsync(EditPositionInputModel input);

        public bool FindByNameAsync(string name);

        public bool FindByIdAsync(string id);

        public IEnumerable<T> GetAll<T>(int? count = null);
    }
}
