using Driver.API.Application.Models;
using Driver.API.Domain.Entities;
using Driver.API.Domain.Interfaces;
using Driver.API.Domain.Results;
using Driver.API.Domain.ValueObjects;
using Driver.API.Infrastructure;

namespace Driver.API.Application
{
    public class DriverCommandHandler : IDriverCommandHandler
    {
        private readonly ILogger<DriverCommandHandler> _logger;
        private readonly IDriverDbContext _dbContext;
        public DriverCommandHandler(ILogger<DriverCommandHandler> logger, IDriverDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public Result<string> Create(DriverCommand driverData)
        {
            Result<string> result = Result<string>.FailureResult("Couldn't add driver..");
            //
            try
            {
                //Map to Domain Driver
                Domain.Entities.Driver driver = new Domain.Entities.Driver
                   (
                   firstName: driverData.firstName,
                   lastName: driverData.lastName,
                   phoneNumber: (PhoneAddress)driverData.phoneNumber,
                   email: (EmailAddress)driverData.email
                   );

                //check email exists
                if (_dbContext.Find(driver.Email)!=null)
                    throw new Domain.Exceptions.EmailAlreadyExistsException(driver.Email.ToString());

                //check phone exists
                if (_dbContext.Find(driver.PhoneNumber) != null)
                    throw new Domain.Exceptions.PhoneNumberAlreadyExistsException(driver.PhoneNumber.ToString());

                //Call Driver Context Execute
                if (_dbContext.Insert(driver) == true)
                    result = Result<string>.SuccessResult(driver.Id.ToString());

            }
            catch (Exception ex)
            {
                result = Result<string>.FailureResult(ex.Message);
                _logger.LogError($"DriverCommandHandler.Create failed due to {ex.Message}");
            }
            return result;
        }
               

        public Result<string> Update(string driverId,DriverCommand driverData)
        {
            Result<string> result = Result<string>.FailureResult("Couldn't update driver..");
            //
            try
            {
                var _driverId = (DriverId)driverId;
                //Map to Domain Driver
                Domain.Entities.Driver driver = new Domain.Entities.Driver
                   (
                    id: _driverId,
                   firstName: driverData.firstName,
                   lastName: driverData.lastName,
                   phoneNumber: (PhoneAddress)driverData.phoneNumber,
                   email: (EmailAddress)driverData.email
                   );
                //check user exists
                var matchWithId = _dbContext.Find(_driverId);
                if (matchWithId == null)
                    throw new Domain.Exceptions.DriverNotFoundException(driver.Id.ToString());

                //check email exists with different user
                var matchWithEmail= _dbContext.Find(driver.Email);
                if (matchWithEmail != null
                    && matchWithEmail.Id!= _driverId)
                    throw new Domain.Exceptions.EmailAlreadyExistsException(driver.Email.ToString());

                //check phone existswith different user
                var matchWithphone = _dbContext.Find(driver.PhoneNumber);
                if (matchWithphone != null
                    && matchWithphone.Id!= _driverId)
                    throw new Domain.Exceptions.PhoneNumberAlreadyExistsException(driver.PhoneNumber.ToString());

                //Call Driver Context Execute
                if (_dbContext.Update(driver) == true)
                    result = Result<string>.SuccessResult(driver.Id.ToString());

            }
            catch (Exception ex)
            {
                result = Result<string>.FailureResult(ex.Message);
                _logger.LogError($"DriverCommandHandler.Update failed due to {ex.Message}");

            }
            return result;
        }
        public Result<string> Delete(string driverId)
        {
            Result<string> result = Result<string>.FailureResult("Couldn't delete driver..");
            //
            try
            {
                var _driverId = (DriverId)driverId;
                //check user exists
                var matchWithId = _dbContext.Find(_driverId);
                if (matchWithId == null)
                    throw new Domain.Exceptions.DriverNotFoundException(driverId);

                //Call Driver Context Execute
                if (_dbContext.Delete(_driverId) == true)
                    result = Result<string>.SuccessResult(driverId);

            }
            catch (Exception ex)
            {
                result = Result<string>.FailureResult(ex.Message);
                _logger.LogError($"DriverCommandHandler.Delete failed due to {ex.Message}");

            }
            return result;
        }
    }
}
