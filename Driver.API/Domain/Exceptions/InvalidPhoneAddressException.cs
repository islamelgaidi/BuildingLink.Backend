namespace Driver.API.Domain.Exceptions
{
    public class InvalidPhoneAddressException:Exception
    {
        private readonly string _phoneNumber;
       public InvalidPhoneAddressException(string phoneNumber) : base($" '{phoneNumber}' is not a valid phone number format. ")
        {
            _phoneNumber = phoneNumber;
        }
        public override string ToString()
        {
            return $" '{_phoneNumber}' is not a valid phone number format. ";
        }
    }
}
