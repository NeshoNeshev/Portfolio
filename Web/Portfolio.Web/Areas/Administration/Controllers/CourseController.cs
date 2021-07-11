﻿namespace Portfolio.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Common;
    using Portfolio.Services.Data;
    using Portfolio.Web.ViewModels.Administration.Course;
    using Portfolio.Web.ViewModels.Administration.Speciality;
    using Portfolio.Web.ViewModels.CourseViewModels;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class CourseController : AdministrationController
    {
        private readonly ISpecialtiesService specialtiesService;
        private readonly ICourseService courseService;
        private readonly IEnumerable<SpecialityDropDown> specialityDropDowns;
        private readonly IEnumerable<CourseDropDown> courseDropDowns;

        public CourseController(ISpecialtiesService specialtiesService, ICourseService courseService)
        {
            this.specialtiesService = specialtiesService;
            this.courseService = courseService;
            this.courseDropDowns = this.courseService.GetAll<CourseDropDown>();
            this.specialityDropDowns = this.specialtiesService.GetAll<SpecialityDropDown>();
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateCourse() => this.View(new CourseInputModel{SpecialtiesDropDowns = this.specialityDropDowns.ToList()});

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCourse(CourseInputModel model)
        {
            var courseName = this.courseService.FindByNameAsync(model.CourseName);
            if (courseName)
            {
                model.SpecialtiesDropDowns = this.specialityDropDowns.ToList();
                this.ModelState.AddModelError(nameof(CourseInputModel.CourseName), $"Exist {model.CourseName}");
                return this.View(model);
            }

            if (!this.ModelState.IsValid)
            {
                model.SpecialtiesDropDowns = this.specialityDropDowns.ToList();
                return this.View(model);
            }

            await this.courseService.CreateAsync(model);
            return this.View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit() => this.View(new EditCourseInputModel{CourseDropDowns = this.courseDropDowns.ToList()});

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditCourseInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.CourseDropDowns = this.courseDropDowns.ToList();
                return this.View(model);
            }

            await this.courseService.UpdateAsync(model);

            return this.Json("asdsa");
        }

        [HttpGet]
        [Authorize]
        public IActionResult AllCourses()
        {
            var model = this.courseService.GetAll<CourseViewModel>();
            var viewModel = new AllCourseViewModel() { CourseViewModels = model };
            return this.View(viewModel);
        }
    }
}
