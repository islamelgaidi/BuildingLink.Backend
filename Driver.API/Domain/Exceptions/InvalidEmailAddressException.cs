namespace Driver.API.Domain.Exceptions
{
    public class InvalidEmailAddressException:Exception
    {
        private readonly string _email;
       public InvalidEmailAddressException(string email) : base($" '{email}' is not a valid email address. ")
        {
            _email = email;
        }
        public override string ToString()
        {
            return $" '{_email}' is not a valid email address. ";
        }
    }
}
