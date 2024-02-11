using Driver.API.Application;
using Driver.API.Application.Models;
using Driver.API.Domain.Interfaces;
using Driver.API.Domain.Results;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Driver.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverQueryHandler _driverQueryHandler;
        private readonly IDriverCommandHandler _driverCommandHandler;

        public DriverController(IDriverQueryHandler driverQueryHandler, IDriverCommandHandler driverCommandHandler)
        {
            _driverQueryHandler = driverQueryHandler;
            _driverCommandHandler = driverCommandHandler;
        }       
        // GET: api/<DriverController>
        [HttpGet]
        public Result<List<DriverQuery>> Get()
        {
            return _driverQueryHandler.GetDrivers(1, 100);
        }

        // GET api/<DriverController>/5
        [HttpGet("{id}")]
        public Result<DriverQuery> Get(string id)
        {
            return _driverQueryHandler.GetDriver(id);
        }

        // POST api/<DriverController>
        [HttpPost]
        public Result<string> Post([FromBody] DriverCommand value)
        {
            return _driverCommandHandler.Create(value);
        }

        // PUT api/<DriverController>/5
        [HttpPut("{id}")]
        public Result<string> Put(string id, [FromBody] DriverCommand value)
        {
            return _driverCommandHandler.Update(id,value);
        }

        // DELETE api/<DriverController>/5
        [HttpDelete("{id}")]
        public Result<string> Delete(string id)
        {
            return _driverCommandHandler.Delete(id);
        }
    }
}
