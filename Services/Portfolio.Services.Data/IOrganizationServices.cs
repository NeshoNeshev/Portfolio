namespace Portfolio.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Portfolio.Web.ViewModels.Administration.Dashboard;
    using Portfolio.Web.ViewModels.Administration.Organization;

    public interface IOrganizationServices
    {
        public IEnumerable<T> GetAll<T>(int? count = null);

        public Task UpdateAsync(EditOrganizationInputModel input);

        Task CreateAsync(OrganizationInputModel input);

        public bool FindByNameAsync(string name);

        public bool FindByIdAsync(string id);

    }
}
