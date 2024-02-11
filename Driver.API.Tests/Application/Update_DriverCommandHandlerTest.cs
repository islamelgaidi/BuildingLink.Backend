using Castle.Core.Logging;
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
    public class Update_DriverCommandHandlerTest
    {
        private DriverCommand GenerateDriverCommand()
        {
            var randvalue = Random.Shared.Next(10000, 99999);
            return  new DriverCommand()
            {
                firstName = "test",
                lastName = $"user{randvalue}",
                email = $"test{randvalue}@gmail.com",
                phoneNumber = $"+1052452{randvalue}"
            };
        }
        private API.Domain.Entities.Driver GenerateDriverDb(DriverCommand driverCommand)
        {
            return new API.Domain.Entities.Driver
                    (
                    firstName: driverCommand.firstName,
                    lastName: driverCommand.lastName,
                    phoneNumber: (PhoneAddress)driverCommand.phoneNumber,
                    email: (EmailAddress)driverCommand.email
                    );
        }
        [Fact]
        public void Update_WithVaildDriverCommand_ReturnsResultSuccess()
        {
            //arrange
            DriverCommand driverCommand = GenerateDriverCommand();

            API.Domain.Entities.Driver driverMatchfromDb = GenerateDriverDb(driverCommand);
            //
            var moqLogger = new Mock<ILogger<DriverCommandHandler>>();

            var moqDriverDbContext = new Mock<IDriverDbContext>();

            moqDriverDbContext.Setup(db => db.Find(It.IsAny<DriverId>()))
                             .Returns(driverMatchfromDb);

            moqDriverDbContext.Setup(db => db.Find(It.IsAny<EmailAddress>()))
                              .Returns<API.Domain.Entities.Driver>(null);

            moqDriverDbContext.Setup(db => db.Find(It.IsAny<PhoneAddress>()))
                              .Returns<API.Domain.Entities.Driver>(null);

            moqDriverDbContext.Setup(m => m.Update(It.IsAny<API.Domain.Entities.Driver>()))
                              .Returns(true);

            DriverCommandHandler commandHandler = new DriverCommandHandler(moqLogger.Object, moqDriverDbContext.Object);
            //act
            Result<string> actual = commandHandler.Update(driverMatchfromDb.Id.ToString(),driverCommand);

            //assert
            Assert.True(actual.Success);
        }
        [Fact]
        public void Create_WithDriverIdNotFound_ReturnsResultFailure()
        {
            //arrange
            DriverCommand driverCommand = GenerateDriverCommand();

            API.Domain.Entities.Driver driverMatchfromDb = GenerateDriverDb(driverCommand);
            //
            var moqLogger = new Mock<ILogger<DriverCommandHandler>>();

            var moqDriverDbContext = new Mock<IDriverDbContext>();

            moqDriverDbContext.Setup(db => db.Find(It.IsAny<DriverId>()))
                           .Returns<API.Domain.Entities.Driver>(null);


            DriverCommandHandler commandHandler = new DriverCommandHandler(moqLogger.Object, moqDriverDbContext.Object);
            //act
            Result<string> actual = commandHandler.Update(driverMatchfromDb.Id.ToString(), driverCommand);

            Result<string> expected = Result<string>.FailureResult(new API.Domain.Exceptions.DriverNotFoundException(driverMatchfromDb.Id.ToString()).Message);
            //assert
            Assert.Equal(expected.Description, actual.Description);
        }
        [Fact]
        public void Create_WithEmailAddressExists_ReturnsResultFailure()
        {
            //arrange
            DriverCommand driverCommand = GenerateDriverCommand();

            API.Domain.Entities.Driver driverMatchfromDb = GenerateDriverDb(driverCommand);
            //
            API.Domain.Entities.Driver anothermatchDb = GenerateDriverDb(GenerateDriverCommand());
            //
            var moqLogger = new Mock<ILogger<DriverCommandHandler>>();

            var moqDriverDbContext = new Mock<IDriverDbContext>();

            moqDriverDbContext.Setup(db => db.Find(It.IsAny<DriverId>()))
                           .Returns(driverMatchfromDb);

            moqDriverDbContext.Setup(db => db.Find(It.IsAny<EmailAddress>()))
                             .Returns(anothermatchDb);           

            DriverCommandHandler commandHandler = new DriverCommandHandler(moqLogger.Object, moqDriverDbContext.Object);
            //act
            Result<string> actual = commandHandler.Update(driverMatchfromDb.Id.ToString(),driverCommand);

            Result<string> expected = Result<string>.FailureResult(new API.Domain.Exceptions.EmailAlreadyExistsException(driverCommand.email).Message);
            //assert
            Assert.Equal(expected.Description,actual.Description);
        }
        [Fact]
        public void Create_WithPhoneNumberExists_ReturnsResultFailure()
        {
            //arrange
            DriverCommand driverCommand = GenerateDriverCommand();

            API.Domain.Entities.Driver driverMatchfromDb = GenerateDriverDb(driverCommand);
            //
            API.Domain.Entities.Driver anothermatchDb = GenerateDriverDb(GenerateDriverCommand());
            //
            var moqLogger = new Mock<ILogger<DriverCommandHandler>>();

            var moqDriverDbContext = new Mock<IDriverDbContext>();

            moqDriverDbContext.Setup(db => db.Find(It.IsAny<DriverId>()))
                           .Returns(driverMatchfromDb);

            moqDriverDbContext.Setup(db => db.Find(It.IsAny<EmailAddress>()))
                             .Returns<API.Domain.Entities.Driver>(null);

            moqDriverDbContext.Setup(db => db.Find(It.IsAny<PhoneAddress>()))
                              .Returns(anothermatchDb);


            DriverCommandHandler commandHandler = new DriverCommandHandler(moqLogger.Object, moqDriverDbContext.Object);
            //act
            Result<string> actual = commandHandler.Update(driverMatchfromDb.Id.ToString(), driverCommand);

            Result<string> expected = Result<string>.FailureResult(new API.Domain.Exceptions.PhoneNumberAlreadyExistsException(driverCommand.phoneNumber).Message);
            //assert
            Assert.Equal(expected.Description, actual.Description);
        }
        [Fact]
        public void Create_WithValidDataButInsertFail_ReturnsResultFailure()
        {
            //arrange
            DriverCommand driverCommand = GenerateDriverCommand();

            API.Domain.Entities.Driver driverMatchfromDb = GenerateDriverDb(driverCommand);
            //
            API.Domain.Entities.Driver anothermatchDb = GenerateDriverDb(GenerateDriverCommand());
            //
            var moqLogger = new Mock<ILogger<DriverCommandHandler>>();

            var moqDriverDbContext = new Mock<IDriverDbContext>();

            moqDriverDbContext.Setup(db => db.Find(It.IsAny<DriverId>()))
                              .Returns(driverMatchfromDb);

            moqDriverDbContext.Setup(db => db.Find(It.IsAny<EmailAddress>()))
                              .Returns<API.Domain.Entities.Driver>(null);

            moqDriverDbContext.Setup(db => db.Find(It.IsAny<PhoneAddress>()))
                              .Returns<API.Domain.Entities.Driver>(null);

            moqDriverDbContext.Setup(m => m.Update(It.IsAny<API.Domain.Entities.Driver>()))
                              .Returns(false);

            DriverCommandHandler commandHandler = new DriverCommandHandler(moqLogger.Object, moqDriverDbContext.Object);
            //act
            Result<string> actual = commandHandler.Update(driverMatchfromDb.Id.ToString(), driverCommand);

            
            //assert
            Assert.False(actual.Success);
        }
    }
}
