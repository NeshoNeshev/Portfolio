namespace Portfolio.Services.Data
{
    using System.Threading.Tasks;

    public interface ICreatePositionService
    {
        Task CreateAsync(string positionName, string moreInformation, string period);

    }
}
