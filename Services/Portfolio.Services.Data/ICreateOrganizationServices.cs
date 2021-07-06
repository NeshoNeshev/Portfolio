using System.Collections.Generic;
using Portfolio.Data.Models;

namespace Portfolio.Services.Data
{
    using System.Threading.Tasks;

    using Portfolio.Web.ViewModels.Administration.Dashboard;

    public interface ICreateOrganizationServices
    {
        public IEnumerable<T> GetAll<T>(int? count = null);

        Task CreateAsync(OrganizationInputModel input);

        public bool FindByNameAsync(string name);

        public bool FindByIdAsync(string id);

       // public Organization GetByName(string name);
    }
}
