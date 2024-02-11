using Driver.API.Application.Models;
using Driver.API.Domain.Results;

namespace Driver.API.Domain.Interfaces
{
    public interface IDriverQueryHandler
    {
        Result<DriverQuery> GetDriver(string Id);

        Result<List<DriverQuery>> GetDrivers(int page,int pageSize);

       

    }
}
