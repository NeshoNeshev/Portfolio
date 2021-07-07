namespace Portfolio.Web.ViewModels.Administration.Speciality
{
    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;

    public class SpecialityDropDown: IMapFrom<Specialty>
    {
        public string Id { get; set; }

        public string SpecialtyName { get; set; }
    }
}
