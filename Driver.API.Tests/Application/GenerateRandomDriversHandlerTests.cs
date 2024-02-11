using Driver.API.Application;
using Driver.API.Application.Models;
using Driver.API.Domain.Interfaces;
using Driver.API.Domain.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.API.Tests.Application
{
    public class GenerateRandomDriversHandlerTests
    {
        [Fact]
        public void GenerateRandomName_call_ReturnsRandomName()
        {
            //arrange
            var moq = new Mock<IDriverCommandHandler>();
            var RandomDriversHandler = new API.Application.GenerateRandomDriversHandler(moq.Object);
            //act
            string actual = RandomDriversHandler.GenerateRandomName();
            //assert
            Assert.NotEmpty(actual);
        }
        [Fact]
        public void GenerateRandomDriverCommand_call_ReturnsDriverCommand()
        {
            //arrange
            var moq = new Mock<IDriverCommandHandler>();
            var RandomDriversHandler = new API.Application.GenerateRandomDriversHandler(moq.Object);
            //act
            DriverCommand actual = RandomDriversHandler.GenerateRandomDriverCommand();
            //assert
            Assert.NotNull(actual);
            Assert.NotEmpty(actual.firstName);
            Assert.NotEmpty(actual.lastName);
        }
        [Fact]
        public void Execute_call_ReturnsSucesResultAndListOfNames()
        {
            //arrange
            var moq = new Mock<IDriverCommandHandler>();
            moq.Setup(x => x.Create(It.IsAny<DriverCommand>()))
                .Returns(Result<string>.SuccessResult("AbcDEF"));
            var RandomDriversHandler = new API.Application.GenerateRandomDriversHandler(moq.Object);
            //act
            Result<List<string>> actual = RandomDriversHandler.Execute();
            //assert
            Assert.True(actual.Success);
            Assert.NotNull(actual.Data);
            Assert.NotEqual(0,actual.Data.Count);
           
        }
    }
}
