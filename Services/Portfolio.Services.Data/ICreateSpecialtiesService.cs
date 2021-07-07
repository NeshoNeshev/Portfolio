namespace Portfolio.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Portfolio.Web.ViewModels.Administration.Speciality;

    public interface ICreateSpecialtiesService
    {
        Task CreateAsync(CreateSpecialtyInputModel model);

        public IEnumerable<T> GetAll<T>(int? count = null);

        public bool FindByNameAsync(string name);

        public bool FindByIdAsync(string id);

        public Task UpdateAsync(EditSpecialityInputModel model);

    }
}
