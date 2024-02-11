using Driver.API.Domain.ValueObjects;

namespace Driver.API.Domain.Exceptions
{
    public class DriverNotFoundException : Exception
    {
        private readonly string _driverId;
        public DriverNotFoundException(string driverId) :
             base($"No driver found with id={driverId}.")
        {
            _driverId = driverId;
        }
        public override string ToString()
        {
            return $"No driver found with id={_driverId}.";
        }
    }
}
