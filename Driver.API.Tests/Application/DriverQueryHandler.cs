using Driver.API.Application;
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
    public class DriverQueryHandler
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
        public void GetDriver_CallWithExistingId_ReturnSuccessAndDriver()
        {
            //arrange 
            API.Domain.Entities.Driver driverMatchfromDb = GenerateDriverDb();

            var moqLogger = new Mock<ILogger<API.Application.DriverQueryHandler>>();

            var moqDriverDbContext = new Mock<IDriverDbContext>();

            moqDriverDbContext.Setup(db => db.Find(It.IsAny<DriverId>()))
                             .Returns(driverMatchfromDb);

            var queryHandler = new  API.Application.DriverQueryHandler(moqLogger.Object, moqDriverDbContext.Object);

            //act
            Result<API.Application.Models.DriverQuery> actual = queryHandler.GetDriver(driverMatchfromDb.Id.ToString());
            Result<API.Application.Models.DriverQuery> expected = Result<API.Application.Models.DriverQuery>.SuccessResult(new API.Application.Models.DriverQuery(driverMatchfromDb));
            //assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetDriver_CallWithNotExistingId_ReturnFailure()
        {
            //arrange 
            API.Domain.Entities.Driver driverMatchfromDb = GenerateDriverDb();

            var moqLogger = new Mock<ILogger<API.Application.DriverQueryHandler>>();

            var moqDriverDbContext = new Mock<IDriverDbContext>();

            moqDriverDbContext.Setup(db => db.Find(It.IsAny<DriverId>()))
                             .Returns<API.Domain.Entities.Driver>(null);

            var queryHandler = new API.Application.DriverQueryHandler(moqLogger.Object, moqDriverDbContext.Object);

            //act
            Result<API.Application.Models.DriverQuery> actual = queryHandler.GetDriver(driverMatchfromDb.Id.ToString());
            Result<API.Application.Models.DriverQuery> expected = Result<API.Application.Models.DriverQuery>.FailureResult(new API.Domain.Exceptions.DriverNotFoundException(driverMatchfromDb.Id.ToString()).Message);
            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDrivers_CallWithDefaultpagging_ReturnSuccessAndDrivers()
        {
            //arrange 
            int page = 1;
            int pageSize = 10;
            List<API.Domain.Entities.Driver> drivers = new() { { GenerateDriverDb() } };
            List<API.Application.Models.DriverQuery> driversQuery = drivers.Select(x => new API.Application.Models.DriverQuery(x)).ToList();
            var moqLogger = new Mock<ILogger<API.Application.DriverQueryHandler>>();

            var moqDriverDbContext = new Mock<IDriverDbContext>();

            moqDriverDbContext.Setup(db => db.FindAll(pageSize, (page-1)*pageSize))
                             .Returns(drivers);

            var queryHandler = new API.Application.DriverQueryHandler(moqLogger.Object, moqDriverDbContext.Object);

            //act
            Result<List<API.Application.Models.DriverQuery>> actual = queryHandler.GetDrivers(page, pageSize);
            Result<List<API.Application.Models.DriverQuery>> expected = Result<List<API.Application.Models.DriverQuery>>.SuccessResult(driversQuery.Take(pageSize).Skip((page - 1) * pageSize).ToList());
            //assert
            Assert.Equal(expected.Data.Count, actual.Data.Count);
        }
       
    }
}
