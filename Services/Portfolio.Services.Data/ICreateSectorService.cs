using System.Collections.Generic;
using Portfolio.Web.ViewModels.Administration.Dashboard;

namespace Portfolio.Services.Data
{
    using System.Threading.Tasks;

    public interface ICreateSectorService
    {
        Task CreateAsync(CreateSectorInputModel model);

        public IEnumerable<T> GetAll<T>(int? count = null);

        public bool FindByNameAsync(string name);

        public bool FindByIdAsync(string id);
        public  Task<T> GetByName<T>(string name);
        public Task UpdateAsync(EditSectorInputModel input);
    }
}
