using System.Collections.Generic;
using Portfolio.Web.ViewModels.Administration.Dashboard;

namespace Portfolio.Services.Data
{
    using System.Threading.Tasks;

    public interface ICreateSectorService
    {
        Task CreateAsync(string sectorName, string organizationName, string positionName, string positionMoreInformation, string positionPeriod);
        public IEnumerable<T> GetAll<T>(int? count = null);
    }
}
