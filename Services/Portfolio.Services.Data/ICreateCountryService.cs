namespace Portfolio.Services.Data
{
    using System.Threading.Tasks;

    public interface ICreateCountryService
    {
        Task CreateAsync(string countryName, string townName);
    }
}
