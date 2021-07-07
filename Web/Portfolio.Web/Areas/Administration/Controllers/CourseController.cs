namespace Portfolio.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Services.Data;
    using Portfolio.Web.ViewModels.Administration.Course;
    using Portfolio.Web.ViewModels.Administration.Speciality;

    public class CourseController : AdministrationController
    {
        private readonly ICreateSpecialtiesService _specialtiesService;
        private readonly ICreateCourseService _courseService;

        public CourseController(ICreateSpecialtiesService specialtiesService, ICreateCourseService courseService)
        {
            _specialtiesService = specialtiesService;
            _courseService = courseService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateCourse()
        {
            var courses = this._specialtiesService.GetAll<SpecialityDropDown>().ToList();
            var viewModel = new CourseInputModel();
            viewModel.SpecialtiesDropDowns = courses;
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCourse(CourseInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            else
            {
                var courseName = this._courseService.FindByNameAsync(model.CourseName);
                if (courseName)
                {
                    this.ModelState.AddModelError(nameof(CourseInputModel.CourseName), $"Exist {model.CourseName}");
                    return this.View(model);
                }

                await this._courseService.CreateAsync(model);
            }
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit()
        {
            var sectors = this._courseService.GetAll<CourseDropDown>().ToList();
            var viewModel = new EditCourseInputModel();
            viewModel.CourseDropDowns = sectors;
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditCourseInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            await this._courseService.UpdateAsync(model);

            return this.Json("asdsa");
        }
    }
}
