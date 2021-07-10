namespace Portfolio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;
    using Portfolio.Web.ViewModels.Administration.Project;

    public class ProjectService : IProjectService
    {
        private readonly IDeletableEntityRepository<Project> projectRepository;
        private readonly IDeletableEntityRepository<PrivateInformation> privatEntityRepository;

        public ProjectService(IDeletableEntityRepository<Project> projectRepository, IDeletableEntityRepository<PrivateInformation> privatEntityRepository)
        {
            this.projectRepository = projectRepository;
            this.privatEntityRepository = privatEntityRepository;
        }

        public async Task CreateAsync(ProjectInputModel input)
        {
            var projectExist = this.projectRepository.All().Any(x => x.ProjectName == input.ProjectName);
            if (projectExist)
            {
                return;
            }

            var privateInformation = this.privatEntityRepository.All().FirstOrDefault(x => x.FirstName == input.PrivateName);
            if (privateInformation == null)
            {
                return;
            }

            var project = new Project
            {
                Id = Guid.NewGuid().ToString(),
                ProjectName = input.ProjectName,
                ImgUrl = input.ImgUrl,
                SiteUrl = input.SiteUrl,
                Content = input.Content,
            };

            project.PrivateInformation = privateInformation;
            await this.projectRepository.AddAsync(project);
            await this.projectRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditProjectInputModel input)
        {
            var project = this.projectRepository
                .All()
                .FirstOrDefault(x => x.Id == input.Id);

            project.ProjectName = input.NewProjectName;
            project.ImgUrl = input.NewImgUrl;
            project.SiteUrl = input.NewSiteUrl;
            project.Content = input.NewContent;
            this.projectRepository.Update(project);
            await this.projectRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Project> query = this.projectRepository.All().OrderBy(x => x.ProjectName);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public bool FindByNameAsync(string name)
            => this.projectRepository
                .All()
                .Any(s => s.ProjectName == name);

        public bool FindByIdAsync(string id) => this.projectRepository
            .All()
            .Any(x => x.Id == id);

    }
}
