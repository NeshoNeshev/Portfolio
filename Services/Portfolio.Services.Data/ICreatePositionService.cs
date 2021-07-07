using System.Collections.Generic;
using Portfolio.Web.ViewModels.Administration.Dashboard;

namespace Portfolio.Services.Data
{
    using System.Threading.Tasks;

    public interface ICreatePositionService
    {
        Task CreateAsync(CreatePositionInputModel model);

        public Task UpdateAsync(EditPositionInputModel input);

        public bool FindByNameAsync(string name);
        public bool FindByIdAsync(string id);

        public IEnumerable<T> GetAll<T>(int? count = null);
    }
}
