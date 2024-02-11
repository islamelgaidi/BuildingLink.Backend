namespace Driver.API.Domain.Exceptions
{
    public class PhoneNumberAlreadyExistsException : Exception
    {
        private readonly string _phone;
       public PhoneNumberAlreadyExistsException(string phone) :
            base($"Phone Number'{phone}' is already  exists")
        {
            _phone = phone;
        }
        public override string ToString()
        {
            return $"Phone Number'{_phone}' is already  exists";
        }
    }
}
