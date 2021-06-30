namespace Portfolio.Web.ViewModels.PrivateInformationViewModel
{
    using Data.Models;
    using Services.Mapping;

    public class PrivateInformationViewModel : IMapFrom<PrivateInformation>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ThirdName { get; set; }

        public string Birthday { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }

        public string Town { get; set; }

        public string Degree { get; set; }
    }
}
