using Driver.API.Domain.Interfaces;
using Driver.API.Domain.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Driver.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateRandomDriversController : ControllerBase
    {
        private readonly IGenerateRandomDriversHandler _randomDriversHandler;

        public GenerateRandomDriversController(IGenerateRandomDriversHandler randomDriversHandler)
        {
            this._randomDriversHandler = randomDriversHandler;
        }
        [HttpPost]
        public Result<List<string>> Post()
        {
            return _randomDriversHandler.Execute();
        }
    }
}
