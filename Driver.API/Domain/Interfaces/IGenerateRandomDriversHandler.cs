using Driver.API.Domain.Results;

namespace Driver.API.Domain.Interfaces
{
    public interface IGenerateRandomDriversHandler
    {
        Result<List<string>> Execute();
    }
}
