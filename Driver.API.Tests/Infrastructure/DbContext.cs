 
using Driver.API.Application;
using Driver.API.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.API.Tests.Infrastructure
{
    public class DbContext
    {
        [Fact]
        public void Execute_CallWithValidQuery_ReturnsValueGreaterthanOrEqualZero()
        {
            //arrange 
            string sql = @" 
            CREATE TABLE IF NOT EXISTS Driver (
                    Id TEXT PRIMARY KEY,
                    FirstName TEXT,
                    LastName TEXT,
                    PhoneNumber TEXT,
                    Email TEXT
                            )";
            var moqLogger = new Mock<ILogger<API.Infrastructure.DbContext>>();
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "DriverConnetionString")])
                           .Returns("Data Source=driver.db");
            var moqConfiguration = new Mock<IConfiguration>();
            moqConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings")))
                            .Returns(mockConfSection.Object);
            //.Returns("Data Source=driver.db");
            var dbcontext = new API.Infrastructure.DbContext(moqConfiguration.Object, moqLogger.Object);
            //act
            int actual = dbcontext.Execute(sql);
            int expected = 0;
            //assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Execute_CallWithInValidQuery_ReturnsValueLessthanZero()
        {
            //arrange 
            string sql = @" 
            CREATE TABLEx IF NOT EXISTS Driver (
                    Id TEXT PRIMARY KEY,
                    FirstName TEXT,
                    LastName TEXT,
                    PhoneNumber TEXT,
                    Email TEXT
                            )";
            var moqLogger = new Mock<ILogger<API.Infrastructure.DbContext>>();
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "DriverConnetionString")])
                           .Returns("Data Source=driver.db");
            var moqConfiguration = new Mock<IConfiguration>();
            moqConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings")))
                            .Returns(mockConfSection.Object);
            //.Returns("Data Source=driver.db");
            var dbcontext = new API.Infrastructure.DbContext(moqConfiguration.Object, moqLogger.Object);
            //act
            int actual = dbcontext.Execute(sql);
            int expected = -1;
            //assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Execute_CallWithValidQueryAndParamters_ReturnsValueGreaterthanOrEqualZero()
        {
            //arrange 
            string sql = @" INSERT INTO driver (Id,FirstName,LastName,Email,PhoneNumber) 
                                        VALUES (@Id,@FirstName,@LastName,@Email,@PhoneNumber)";
            var moqLogger = new Mock<ILogger<API.Infrastructure.DbContext>>();
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "DriverConnetionString")])
                           .Returns("Data Source=driver.db");
            var moqConfiguration = new Mock<IConfiguration>();
            moqConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings")))
                            .Returns(mockConfSection.Object);
            var randvalue = Random.Shared.Next(10000, 99999);
            var parameters = new Dictionary<string, object>()
            {
                { "@Id", Guid.NewGuid().ToString() } ,
                { "@FirstName", "Test" } ,
                { "@LastName", $"user{randvalue}" } ,
                { "@Email", $"testuser{randvalue}@gmail.com" } ,
                { "@PhoneNumber", $"+125478{randvalue}" } ,
            };
            var dbcontext = new API.Infrastructure.DbContext(moqConfiguration.Object, moqLogger.Object);
            //act
            int actual = dbcontext.Execute(sql, parameters);
            int expected = 1;
            //assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Execute_CallWithInValidQueryAndParameters_ReturnsValueLessthanZero()
        {
            //arrange 
            string sql = @" UPDATE driver SET FirstName='test' WHERE Id=@Id";
            var moqLogger = new Mock<ILogger<API.Infrastructure.DbContext>>();
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "DriverConnetionString")])
                           .Returns("Data Source=driver.db");
            var moqConfiguration = new Mock<IConfiguration>();
            moqConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings")))
                            .Returns(mockConfSection.Object);
            var parameters = new Dictionary<string, object>() { };
            var dbcontext = new API.Infrastructure.DbContext(moqConfiguration.Object, moqLogger.Object);
            //act
            int actual = dbcontext.Execute(sql, parameters);
            int expected = -1;
            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTable_CallWithValidQuery_ReturnsTableWithRowCountGreaterthanOrEqualZero()
        {
            //arrange 
            string sql = @"SELECT * FROM driver LIMIT 1";
            var moqLogger = new Mock<ILogger<API.Infrastructure.DbContext>>();
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "DriverConnetionString")])
                           .Returns("Data Source=driver.db");
            var moqConfiguration = new Mock<IConfiguration>();
            moqConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings")))
                            .Returns(mockConfSection.Object);
            //.Returns("Data Source=driver.db");
            var dbcontext = new API.Infrastructure.DbContext(moqConfiguration.Object, moqLogger.Object);
            //act
            int actual = dbcontext.GetDataTable(sql).Rows.Count;
            int expected = 1;
            //assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetTable_CallWithInValidQuery_ReturnsTableWithRowCountEqualZero()
        {
            //arrange 
            string sql = @"SELECT * FROM driver LIMI 1";
            var moqLogger = new Mock<ILogger<API.Infrastructure.DbContext>>();
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "DriverConnetionString")])
                           .Returns("Data Source=driver.db");
            var moqConfiguration = new Mock<IConfiguration>();
            moqConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings")))
                            .Returns(mockConfSection.Object);
            //.Returns("Data Source=driver.db");
            var dbcontext = new API.Infrastructure.DbContext(moqConfiguration.Object, moqLogger.Object);
            //act
            int actual = dbcontext.GetDataTable(sql).Rows.Count;
            int expected = 0;
            //assert
            Assert.Equal(expected, actual);
        }
    }
}
