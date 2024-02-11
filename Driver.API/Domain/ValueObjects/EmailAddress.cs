using Driver.API.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Driver.API.Domain.ValueObjects
{
    public record EmailAddress
    {
        private readonly string _value;
        public const string RegexPattern = "[^@ \\t\\r\\n]+@[^@ \\t\\r\\n]+\\.[^@ \\t\\r\\n]+";
        public EmailAddress(string email)
        {         
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email), $"{nameof(email)} can't be empty.");
            //
            if (IsValid(email) == false)
                throw new InvalidEmailAddressException(email);
            
            //
            _value = email;
        }
        public static bool IsValid(string email)=> Regex.Match(email, RegexPattern).Success;
       
        public static explicit operator EmailAddress(string value) =>new EmailAddress(value);

        public static implicit operator string(EmailAddress email) => email._value;

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
