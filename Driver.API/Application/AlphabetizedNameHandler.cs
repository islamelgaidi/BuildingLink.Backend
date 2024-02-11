using Driver.API.Application.Models;
using Driver.API.Domain.Interfaces;
using Driver.API.Domain.Results;
using Driver.API.Domain.ValueObjects;

namespace Driver.API.Application
{
    public class AlphabetizedNameHandler : IAlphabetizedNameHandler
    {
        private readonly IDriverQueryHandler _queryHandler;

        public AlphabetizedNameHandler(IDriverQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
        }

        public string Alphabetize(string name)
        {
            return string.Concat(name.OrderBy(x => x.ToString().ToLower()));
        }

        public Result<string> GetDriver(string driverId)
        {
            Result<DriverQuery> result = _queryHandler.GetDriver(driverId);
            if (result.Success == false)
                return Result<string>.FailureResult(result.Description);
            //
            return Result<string>.SuccessResult($"{Alphabetize(result.Data.firstName)} {Alphabetize(result.Data.lastName)}");
        }

        public Result<List<string>> GetDrivers(int page, int pageSize)
        {
            Result<List<DriverQuery>> result = _queryHandler.GetDrivers(page, pageSize);
            if (result.Success == false)
                return Result<List<string>>.FailureResult(result.Description);
            //
            return Result<List<string>>.SuccessResult(result.Data.Select(x => $"{Alphabetize(x.firstName)} {Alphabetize(x.lastName)}").ToList());
        }
    }
}
