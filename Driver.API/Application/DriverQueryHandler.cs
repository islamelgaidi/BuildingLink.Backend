using Driver.API.Application.Models;
using Driver.API.Domain.Entities;
using Driver.API.Domain.Interfaces;
using Driver.API.Domain.Results;
using Driver.API.Domain.ValueObjects;

namespace Driver.API.Application
{
    public class DriverQueryHandler : IDriverQueryHandler
    {
        private readonly ILogger<DriverQueryHandler> _logger;
        private readonly IDriverDbContext _dbContext;

        public DriverQueryHandler(ILogger<DriverQueryHandler> logger, IDriverDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public Result<DriverQuery> GetDriver(string driverId)
        {
            Result<DriverQuery> result= Result<DriverQuery>.FailureResult($"Couldn't Find driver with Id={driverId}");
            try
            {
                Domain.Entities.Driver driver = _dbContext.Find((DriverId)driverId);
                if (driver == null)
                    throw new Domain.Exceptions.DriverNotFoundException(driverId);
                //
                result = Result<DriverQuery>.SuccessResult(new DriverQuery(driver));

            }
            catch(Exception ex)
            {
                result = Result<DriverQuery>.FailureResult(ex.Message);
            }
            return result;
        }
              

        public Result<List<DriverQuery>> GetDrivers(int page=1, int pageSize = 100)
        {
            Result<List<DriverQuery>> result = Result<List<DriverQuery>>.FailureResult($"Couldn't Find drivers ");
            try
            {
                List<Domain.Entities.Driver> drivers = _dbContext.FindAll(pageSize, pageSize * (page-1));
                
                result = Result<List<DriverQuery>>.SuccessResult(drivers.Select(d => new DriverQuery(d)).ToList());
            }
            catch (Exception ex)
            {
                result = Result<List<DriverQuery>>.FailureResult(ex.Message);
            }
            return result;
        }

       
    }
}
