using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Data.Common.Models;
using Portfolio.Data.Common.Repositories;
using Portfolio.Data.Models;
using Portfolio.Services.Mapping;
using Portfolio.Web.ViewModels.Administration.Dashboard;
using Portfolio.Web.ViewModels.Administration.Organization;
using Portfolio.Web.ViewModels.Administration.Project;

namespace Portfolio.Services.Data
{
    public class ProjectService : IProjectService
    {
        private readonly IDeletableEntityRepository<Project> _projectRepository;
        private readonly IDeletableEntityRepository<PrivateInformation> _privatEntityRepository;

        public ProjectService(IDeletableEntityRepository<Project> projectRepository, IDeletableEntityRepository<PrivateInformation> privatEntityRepository)
        {
            _projectRepository = projectRepository;
            _privatEntityRepository = privatEntityRepository;
        }

        public async Task CreateAsync(ProjectInputModel input)
        {
            var projectExist = this._projectRepository.All().Any(x => x.ProjectName == input.ProjectName);
            if (projectExist)
            {
                return;
            }

            var privateInformation = this._privatEntityRepository.All().FirstOrDefault(x => x.FirstName == input.PrivateName);
            if (privateInformation == null)
            {
                return;
            }

            var project = new Project
            {
                Id = Guid.NewGuid().ToString(),
                ProjectName = input.ProjectName,
                ImgUrl = input.ImgUrl,
            };

            project.PrivateInformation = privateInformation;
            await this._projectRepository.AddAsync(project);
            await this._projectRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditProjectInputModel input)
        {
            var project = this._projectRepository
                .All()
                .FirstOrDefault(x => x.Id == input.Id);

            project.ProjectName = input.NewProjectName;
            project.ImgUrl = input.NewProjectUrl;
            this._projectRepository.Update(project);
            await this._projectRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Project> query = this._projectRepository.All().OrderBy(x => x.ProjectName);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public bool FindByNameAsync(string name)
            => this._projectRepository
                .All()
                .Any(s => s.ProjectName == name);

        public bool FindByIdAsync(string id) => this._projectRepository
            .All()
            .Any(x => x.Id == id);

    }
}
