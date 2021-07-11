namespace Portfolio.Services.Data
{
    using System.Threading.Tasks;

    public interface ICountryService
    {
        Task CreateAsync(string countryName, string townName);

        public bool FindByNameAsync(string name);
    }
}
