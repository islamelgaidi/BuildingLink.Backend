using System.Numerics;

namespace Driver.API.Domain.Exceptions
{
    public class EmailAlreadyExistsException:Exception
    {
        private readonly string _email;
       public EmailAlreadyExistsException(string email) : 
            base($"Email'{email}' is already  exists")
        { 
            _email=email;
        }
        public override string ToString()
        {
            return $"Email'{_email}' is already  exists";
        }
    }
}
