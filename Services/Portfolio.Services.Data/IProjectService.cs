using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Web.ViewModels.Administration.Project;

namespace Portfolio.Services.Data
{
    public interface IProjectService
    {
        public Task CreateAsync(ProjectInputModel input);

        public Task UpdateAsync(EditProjectInputModel input);

        public IEnumerable<T> GetAll<T>(int? count = null);

        public bool FindByNameAsync(string name);

        public bool FindByIdAsync(string id);
    }
}
