using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.Data
{
    public interface ICreateCertificatesService
    {
        Task CreateAsync(string name, string link, string description, string date, string cName );
    }
}
