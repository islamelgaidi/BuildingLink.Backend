using Driver.API.Domain.Exceptions;
using Driver.API.Domain.ValueObjects;


namespace Driver.API.Tests.Domain.Entities
{
    public class DriverTests
    {
        [Fact]
        public void Constructor_CreateWithValidValues_Sucess()
        {
            //arrange
            string validFirstname = "Mahomud";
            string validLastName = "Ali";
            string validEmail = "mali@gmail.com";
            string validPhoneNumber = "+105215478526";
            //act
            API.Domain.Entities.Driver actual = new API.Domain.Entities.Driver
            (
            firstName: validFirstname,
            lastName: validLastName,
            email: new EmailAddress(validEmail),
            phoneNumber: new PhoneAddress(validPhoneNumber)
            );
            //assert
            Assert.NotNull(actual);
        }
        [Fact]
        public void Constructor_CreateWithEmptyFirstName_FireArgumentNullException()
        {
            //arrange
            string emptyFirstname = "";
            string validLastName = "Ali";
            string validEmail = "mali@gmail.com";
            string validPhoneNumber = "+105215478526";
            //act
            Func<API.Domain.Entities.Driver> actual = () => new API.Domain.Entities.Driver
             (
             firstName: emptyFirstname,
             lastName: validLastName,
             email: new EmailAddress(validEmail),
             phoneNumber: new PhoneAddress(validPhoneNumber)
             );
            //assert
            Assert.Throws<ArgumentNullException>(actual);
        }        
        [Fact]
        public void Constructor_CreateWithEmptyLastName_FireArgumentNullException()
        {
            //arrange
            string vaildFirstname = "Mohmoud";
            string emptyLastName = "";
            string validEmail = "mali@gmail.com";
            string validPhoneNumber = "+105215478526";
            //act
            Func<API.Domain.Entities.Driver> actual = () => new API.Domain.Entities.Driver
             (
             firstName: vaildFirstname,
             lastName: emptyLastName,
             email: new EmailAddress(validEmail),
             phoneNumber: new PhoneAddress(validPhoneNumber)
             );
            //assert
            Assert.Throws<ArgumentNullException>(actual);
        }
        [Fact]
        public void Constructor_CreateWithEmptyEmail_FireArgumentNullException()
        {
            //arrange
            string vaildFirstname = "Mohmoud";
            string validLastName = "Ali";
            string emptyEmail = "";
            string validPhoneNumber = "+105215478526";
            //act
            Func<API.Domain.Entities.Driver> actual = () => new API.Domain.Entities.Driver
             (
             firstName: vaildFirstname,
             lastName: validLastName,
             email: new EmailAddress(emptyEmail),
             phoneNumber: new PhoneAddress(validPhoneNumber)
             );
            //assert
            Assert.Throws<ArgumentNullException>(actual);
        }      
        [Fact]
        public void Constructor_CreateWithInvalidEmail_FireInvalidEmailAddressException()
        {
            //arrange
            string vaildFirstname = "Mohmoud";
            string validLastName = "Ali";
            string invalidEmail = "mali@gmailcom";
            string validPhoneNumber = "+105215478526";
            //act
            Func<API.Domain.Entities.Driver> actual = () => new API.Domain.Entities.Driver
             (
             firstName: vaildFirstname,
             lastName: validLastName,
             email: new EmailAddress(invalidEmail),
             phoneNumber: new PhoneAddress(validPhoneNumber)
             );
            //assert
            Assert.Throws<InvalidEmailAddressException>(actual);
        }

        [Fact]
        public void Constructor_CreateWithEmptyPhone_FireArgumentNullException()
        {
            //arrange
            string vaildFirstname = "Mohmoud";
            string validLastName = "Ali";
            string validEmail = "mali@gmail.com";
            string emptyPhoneNumber = "";
            //act
            Func<API.Domain.Entities.Driver> actual = () => new API.Domain.Entities.Driver
             (
             firstName: vaildFirstname,
             lastName: validLastName,
             email: new EmailAddress(validEmail),
             phoneNumber: new PhoneAddress(emptyPhoneNumber)
             );
            //assert
            Assert.Throws<ArgumentNullException>(actual);
        }
        [Fact]
        public void Constructor_CreateWithInvalidPhone_FireInvalidPhoneAddressException()
        {
            //arrange
            string vaildFirstname = "Mohmoud";
            string validLastName = "Ali";
            string validEmail = "mali@gmail.com";
            string invalidPhoneNumber = "+1052154785";
            //act
            Func<API.Domain.Entities.Driver> actual = () => new API.Domain.Entities.Driver
             (
             firstName: vaildFirstname,
             lastName: validLastName,
             email: new EmailAddress(validEmail),
             phoneNumber: new PhoneAddress(invalidPhoneNumber)
             );
            //assert
            Assert.Throws<InvalidPhoneAddressException>(actual);
        }
    }
}
