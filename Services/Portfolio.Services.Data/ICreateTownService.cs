namespace Portfolio.Services.Data
{
    using System.Threading.Tasks;

    public interface ICreateTownService
    {
        Task CreateAsync(string townName, string countryName);
    }
}
