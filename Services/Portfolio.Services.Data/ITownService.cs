namespace Portfolio.Services.Data
{
    using System.Threading.Tasks;

    public interface ITownService
    {
        Task CreateAsync(string townName, string countryName);
    }
}
