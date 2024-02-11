using Driver.API.Domain.Results;

namespace Driver.API.Domain.Interfaces
{
    public interface IAlphabetizedNameHandler
    {
        Result<List<string>> GetDrivers(int page, int pageSize);
        Result<string> GetDriver(string Id);        
    }
}
