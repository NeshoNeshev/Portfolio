namespace Portfolio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;
    using Portfolio.Web.ViewModels.Administration.Course;

    public class CourseService : ICourseService
    {
        private readonly IDeletableEntityRepository<Specialty> specialityRepository;
        private readonly IDeletableEntityRepository<Course> courseRepository;

        public CourseService(IDeletableEntityRepository<Specialty> specialityRepository,
            IDeletableEntityRepository<Course> courseRepository)
        {
            this.specialityRepository = specialityRepository;
            this.courseRepository = courseRepository;
        }

        public async Task CreateAsync(CourseInputModel model)
        {

            var specialty = this.specialityRepository.All().FirstOrDefault(x => x.Id == model.SpecialtyId);
            if (specialty == null)
            {
                return;
            }

            var course = new Course
            {
                Id = Guid.NewGuid().ToString(),
                CourseName = model.CourseName,
                Description = model.Description,
                Date = model.Date,
                Specialty = specialty,
            };
            await this.courseRepository.AddAsync(course);
            await this.courseRepository.SaveChangesAsync();
            if (course.Certificates.Count > 0)
            {
                return;
            }
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Course> query = this.courseRepository.All().OrderBy(x => x.CourseName);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public bool FindByNameAsync(string name)
            => this.courseRepository
                .All()
                .Any(s => s.CourseName == name);

        public bool FindByIdAsync(string id) => this.courseRepository
            .All()
            .Any(x => x.Id == id);

        public async Task UpdateAsync(EditCourseInputModel input)
        {
            var course = this.courseRepository
                .All()
                .FirstOrDefault(x => x.Id == input.Id);

            course.CourseName = input.NewCourseName;
            course.Description = input.NewDescription;
            course.Date = input.Date;
            this.courseRepository.Update(course);
            await this.courseRepository.SaveChangesAsync();
        }
    }
}
