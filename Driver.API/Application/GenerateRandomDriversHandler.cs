using Driver.API.Application.Models;
using Driver.API.Domain.Interfaces;
using Driver.API.Domain.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text;

namespace Driver.API.Application
{
    public class GenerateRandomDriversHandler : IGenerateRandomDriversHandler
    {
        private readonly IDriverCommandHandler _commandhandler;

        public GenerateRandomDriversHandler(IDriverCommandHandler commandhandler)
        {
            _commandhandler = commandhandler;
        }

        public Result<List<string>> Execute()
        {
            List<string> names = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                DriverCommand driverCommand = GenerateRandomDriverCommand();
                if (_commandhandler.Create(driverCommand).Success)
                {
                    names.Add($"{driverCommand.firstName} {driverCommand.lastName}");
                }
            }
            return Result<List<string>>.SuccessResult(names);
        }
        public DriverCommand GenerateRandomDriverCommand() 
        {
            int randomValue = Random.Shared.Next(10000, 99999);
            string _firstName = GenerateRandomName();
            string _lastName = GenerateRandomName();
            return new DriverCommand()
            {
                firstName = _firstName,
                lastName = _lastName,
                email = $"{_lastName}{randomValue}@gmail.com",
                phoneNumber = $"+1025478{randomValue}"
            };
        }
        public string GenerateRandomName()
        {
            StringBuilder namebuilder = new StringBuilder();
                        
            string Chars = "abcdefghijklmnopqrstuvwxyz";
            for (int i = 0; i < 6; i++)
            {
                namebuilder.Append(Chars[Random.Shared.Next(0, 25)]);
            }
            return namebuilder.ToString();
        }
    }
}
