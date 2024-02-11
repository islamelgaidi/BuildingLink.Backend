using Driver.API.Domain.Exceptions;
using Driver.API.Domain.ValueObjects;

namespace Driver.API.Tests.Domain.ValueObjects
{
    public class EmailAddressTests
    {
        [Fact]
        public void Constructor_CreateWithVaildValue_Sucess()
        {
            //arrange
            string ValidEmail = "jordan@gmail.com";
            //act
            EmailAddress actual = new EmailAddress(ValidEmail);
            //assert
            Assert.NotNull(actual);
        }
        [Fact]
        public void Constructor_CreateWithEmptyValue_FireArgumentNullException()
        {
            //arrange
            string ValidEmail = "";
            //act
            Func<EmailAddress> actual = () => new EmailAddress(ValidEmail);
            //assert
            Assert.Throws<ArgumentNullException>(actual);
        }
        [Fact]
        public void Constructor_CreateWithInvalidValue_FireInvalidEmailException()
        {
            //arrange
            string InvalidEmail = "joe@gmai";
            //act
            Func<EmailAddress> actual = () => new EmailAddress(InvalidEmail);
            //assert
            Assert.Throws<InvalidEmailAddressException>(actual);
        }

        [Fact]
        public void IsValid_CallWithVaildValue_ReturnTrue()
        {
            //arrange
            string ValidEmail = "jordan@gmail.com";
            //act
            bool actual = EmailAddress.IsValid(ValidEmail);
            bool expected = true;
            //assert
            Assert.Equal(expected, actual);

        }
        [Fact]
        public void IsVaild_CallWithInvalidValue_ReturnFalse()
        {
            //arrange
            string ValidEmail = "jordangmail.com";
            //act
            bool actual = EmailAddress.IsValid(ValidEmail);
            bool expected = false;
            //assert
            Assert.Equal(expected, actual);
        }
    }
}