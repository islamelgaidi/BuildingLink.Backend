using Driver.API.Application.Models;
using Driver.API.Domain.Interfaces;
using Driver.API.Domain.Results;
using Microsoft.AspNetCore.Mvc;

namespace Driver.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DriverAlphabetizedNameController : ControllerBase
    {
        private readonly IAlphabetizedNameHandler _alphabetizedNameHandler;

        public DriverAlphabetizedNameController(IAlphabetizedNameHandler alphabetizedNameHandler)
        {
            _alphabetizedNameHandler = alphabetizedNameHandler;
        }

        // GET: api/<DriverController>
        [HttpGet]
        public Result<List<string>> Get()
        {
            return _alphabetizedNameHandler.GetDrivers(1, 100);
        }

        // GET api/<DriverController>/5
        [HttpGet("{id}")]
        public Result<string> Get(string id)
        {
            return _alphabetizedNameHandler.GetDriver(id);
        }
    }
}
