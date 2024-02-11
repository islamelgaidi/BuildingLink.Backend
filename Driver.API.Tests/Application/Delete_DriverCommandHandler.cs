using Driver.API.Application;
using Driver.API.Application.Models;
using Driver.API.Domain.Interfaces;
using Driver.API.Domain.Results;
using Driver.API.Domain.ValueObjects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.API.Tests.Application
{
    public class Delete_DriverCommandHandler
    {
        private API.Domain.Entities.Driver GenerateDriverDb()
        {
            var randvalue = Random.Shared.Next(10000, 99999);
            return new API.Domain.Entities.Driver
                    (
                        firstName: "test",
                        lastName: $"user{randvalue}",
                        email: (EmailAddress)$"test{randvalue}@gmail.com",
                        phoneNumber: (PhoneAddress)$"+1052452{randvalue}"
                    );
        }
        [Fact]
        public void Delete_CallWithExistingId_ReturnSuccesResult()
        {
            API.Domain.Entities.Driver driverMatchfromDb = GenerateDriverDb();
            //arrange
            var moqLogger = new Mock<ILogger<DriverCommandHandler>>();

            var moqDriverDbContext = new Mock<IDriverDbContext>();

            moqDriverDbContext.Setup(db => db.Find(It.IsAny<DriverId>()))
                             .Returns(driverMatchfromDb);

            moqDriverDbContext.Setup(db => db.Delete(It.IsAny<DriverId>()))
                           .Returns(true);

            DriverCommandHandler commandHandler = new DriverCommandHandler(moqLogger.Object, moqDriverDbContext.Object);
            //act
            Result<string> actual = commandHandler.Delete(driverMatchfromDb.Id.ToString());
            //assert
            Assert.True(actual.Success);
        }
        [Fact]
        public void Delete_CallWithNotExistingId_ReturnFailureResult()
        {
            API.Domain.Entities.Driver driverMatchfromDb = GenerateDriverDb();
            //arrange
            var moqLogger = new Mock<ILogger<DriverCommandHandler>>();

            var moqDriverDbContext = new Mock<IDriverDbContext>();

            moqDriverDbContext.Setup(db => db.Find(It.IsAny<DriverId>()))
                             .Returns<API.Domain.Entities.Driver>(null);                      

            DriverCommandHandler commandHandler = new DriverCommandHandler(moqLogger.Object, moqDriverDbContext.Object);
            //act
            Result<string> actual = commandHandler.Delete(driverMatchfromDb.Id.ToString());
            Result<string> expected = Result<string>.FailureResult(new API.Domain.Exceptions.DriverNotFoundException(driverMatchfromDb.Id.ToString()).Message);
            //assert
            Assert.Equal(expected.Description,actual.Description);
        }
        [Fact]
        public void Delete_CallWithExistingIdButFails_ReturnFailureResult()
        {
            API.Domain.Entities.Driver driverMatchfromDb = GenerateDriverDb();
            //arrange
            var moqLogger = new Mock<ILogger<DriverCommandHandler>>();

            var moqDriverDbContext = new Mock<IDriverDbContext>();

            moqDriverDbContext.Setup(db => db.Find(It.IsAny<DriverId>()))
                             .Returns(driverMatchfromDb);

            moqDriverDbContext.Setup(db => db.Delete(It.IsAny<DriverId>()))
                           .Returns(false);

            DriverCommandHandler commandHandler = new DriverCommandHandler(moqLogger.Object, moqDriverDbContext.Object);
            //act
            Result<string> actual = commandHandler.Delete(driverMatchfromDb.Id.ToString());
            //assert
            Assert.False(actual.Success);
        }
    }
}
