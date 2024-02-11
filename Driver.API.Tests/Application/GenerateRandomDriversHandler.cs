using Driver.API.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.API.Tests.Application
{
    public class GenerateRandomDriversHandler
    {
        [Fact]
        public void GenerateRandomName_Call_ReturnNotEmptyString() 
        {
            //arrange
            var moq = new Moq.Mock<IDriverCommandHandler>();
            var generateDriverHandler = new API.Application.GenerateRandomDriversHandler(moq.Object);
            //act
            string actual=generateDriverHandler.GenerateRandomName();
            //assert
            Assert.NotEmpty(actual);
        }
    }
}
