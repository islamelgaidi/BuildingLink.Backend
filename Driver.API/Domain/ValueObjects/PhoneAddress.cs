using Driver.API.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Driver.API.Domain.ValueObjects
{
    public record PhoneAddress
    {
        private readonly string _value;
        public const string RegxPattern= "^\\+(?:[0-9]●?){10,14}[0-9]$";

        public PhoneAddress(string phoneNumber)
        {
            //
            if(string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber), $"{nameof(phoneNumber)} can't be empty.");
            //
            if (IsValid(phoneNumber) == false)
                throw new InvalidPhoneAddressException(phoneNumber);
            //
            _value = phoneNumber;
        }
        public static bool IsValid(string number)=>Regex.Match(number, RegxPattern).Success;

        public static explicit operator PhoneAddress(string phoneNumber) => new PhoneAddress(phoneNumber);

        public static implicit operator string(PhoneAddress phone) => phone._value;
        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
