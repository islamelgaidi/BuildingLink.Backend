using Driver.API.Application;
using Driver.API.Application.Models;
using Driver.API.Domain.Interfaces;
using Driver.API.Domain.Results;
using Driver.API.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.API.Tests.Application
{
    public class AlphabetizedNameHandlerTests
    {
        [Fact]
        public void Alphabetize_CallWithText_ReturnsAlphabetizedText()
        {
            //arrange 
            var test = "Ooliver";
            var moq = new Moq.Mock<IDriverQueryHandler>();
            var alphabetizedName = new AlphabetizedNameHandler(moq.Object);
            //act
            var actual = alphabetizedName.Alphabetize(test);
            var expected = "eilOorv";
            //assert
            Assert.Equal(expected,actual);
        }

        [Fact]
        public void GetDriver_CallWithExistingId_ReturnsDriverAlphabetizedName()
        {
            //arrange 
            var driverId = Guid.NewGuid().ToString();
            var moq = new Moq.Mock<IDriverQueryHandler>();
            DriverQuery driverQuery = new DriverQuery()
            {
                firstName= "Oliver",
                lastName="Can",
                email="Oliver@gmail.com",
                phoneNumber="+145025487521"
            };

            moq.Setup(x => x.GetDriver(driverId))
               .Returns(Result<DriverQuery>.SuccessResult(driverQuery));

            var alphabetizedName = new AlphabetizedNameHandler(moq.Object);
            //act
            var actual = alphabetizedName.GetDriver(driverId);
            var expected = "eilOrv aCn";
            //assert
            Assert.Equal(expected, actual.Data);
            Assert.True(actual.Success);
        }
        [Fact]
        public void GetDriver_CallWithNotExistingId_ReturnsFailureResultAndEmptyName()
        {
            //arrange 
            var driverId = Guid.NewGuid().ToString();
            var moq = new Moq.Mock<IDriverQueryHandler>();
            DriverQuery driverQuery = new DriverQuery()
            {
                firstName = "Oliver",
                lastName = "Can",
                email = "Oliver@gmail.com",
                phoneNumber = "+145025487521"
            };

            moq.Setup(x => x.GetDriver(driverId))
               .Returns(Result<DriverQuery>.FailureResult(""));

            var alphabetizedName = new AlphabetizedNameHandler(moq.Object);
            //act
            var actual = alphabetizedName.GetDriver(driverId);
            var expected = "eilOrv aCn";
            //assert
            Assert.NotEqual(expected, actual.Data);
            Assert.False(actual.Success);
        }

        [Fact]
        public void GetDrivers_CallWithExistingId_ReturnsDriverAlphabetizedNames()
        {
            //arrange            
            var moq = new Moq.Mock<IDriverQueryHandler>();
            List<DriverQuery> driversQuery = new List<DriverQuery>{ new DriverQuery()
            {
                firstName = "Oliver",
                lastName = "Can",
                email = "Oliver@gmail.com",
                phoneNumber = "+145025487521"
            } };

            moq.Setup(x => x.GetDrivers(1, 10))
               .Returns(Result<List<DriverQuery>>.SuccessResult(driversQuery));

            var alphabetizedName = new AlphabetizedNameHandler(moq.Object);
            //act
            var actual = alphabetizedName.GetDrivers(1, 10);
            var expected = "eilOrv aCn";
            //assert
            Assert.Equal(expected, actual.Data.FirstOrDefault());
            Assert.True(actual.Success);
        }
      
    }
}
