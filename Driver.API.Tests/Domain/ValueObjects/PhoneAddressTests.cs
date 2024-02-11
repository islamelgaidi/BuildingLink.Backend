using Driver.API.Domain.Exceptions;
using Driver.API.Domain.ValueObjects;

namespace Driver.API.Tests.Domain.ValueObjects
{
    public class PhoneAddressTests
    {
        [Fact]
        public void Constructor_CreateWithVaildValue_Sucess()
        {
            //arrange
            string ValidPhone = "+201278521548";
            //act
            PhoneAddress actual = new PhoneAddress(ValidPhone);
            //assert
            Assert.NotNull(actual);
        }
        [Fact]
        public void Constructor_CreateWithEmptyValue_FireArgumentNullException()
        {
            //arrange
            string ValidPhone = "";
            //act
            Func<PhoneAddress> actual = () => new PhoneAddress(ValidPhone);
            //assert
            Assert.Throws<ArgumentNullException>(actual);
        }
        [Fact]
        public void Constructor_CreateWithInvalidValue_FireInvalidPhoneException()
        {
            //arrange
            string InvalidPhone = "005555551234";
            //act
            Func<PhoneAddress> actual = () => new PhoneAddress(InvalidPhone);
            //assert
            Assert.Throws<InvalidPhoneAddressException>(actual);
        }

        [Fact]
        public void IsValid_CallWithVaildValue_ReturnTrue()
        {
            //arrange
            string ValidPhone = "+15555551234";
            //act
            bool actual = PhoneAddress.IsValid(ValidPhone);
            bool expected = true;
            //assert
            Assert.Equal(expected, actual);

        }
        [Fact]
        public void IsVaild_CallWithInvalidValue_ReturnFalse()
        {
            //arrange
            string ValidPhone = "+5555551234";
            //act
            bool actual = PhoneAddress.IsValid(ValidPhone);
            bool expected = false;
            //assert
            Assert.Equal(expected, actual);
        }
    }
}