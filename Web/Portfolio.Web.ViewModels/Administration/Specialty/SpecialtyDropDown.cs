namespace Portfolio.Web.ViewModels.Administration.Specialty
{
    using Portfolio.Data.Models;
    using Portfolio.Services.Mapping;

    public class SpecialtyDropDown: IMapFrom<Specialty>
    {
        public string Id { get; set; }

        public string SpecialtyName { get; set; }
    }
}
