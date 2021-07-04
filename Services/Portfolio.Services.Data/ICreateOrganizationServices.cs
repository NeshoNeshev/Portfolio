using System.Collections.Generic;

namespace Portfolio.Services.Data
{
    using System.Threading.Tasks;

    using Portfolio.Web.ViewModels.Administration.Dashboard;

    public interface ICreateOrganizationServices
    {
        public IEnumerable<T> GetAll<T>(int? count = null);
        Task CreateAsync(OrganizationInputModel input);
    }
}
