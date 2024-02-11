using Driver.API.Application.Models;
using Driver.API.Domain.Results;
using Driver.API.Domain.ValueObjects;

namespace Driver.API.Domain.Interfaces
{
    public interface IDriverCommandHandler
    {
        Result<string> Create(DriverCommand driverData);
        Result<string> Update(string driverId, DriverCommand driverData);
        Result<string> Delete(string  driverId);
    }
}
