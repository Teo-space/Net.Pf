using Api.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiKeyAuthAttributeTestController : ControllerBase
    {
        private readonly ILogger<ApiKeyAuthAttributeTestController> _logger;

        public ApiKeyAuthAttributeTestController(ILogger<ApiKeyAuthAttributeTestController> logger)
        {
            _logger = logger;
        }


        [ApiKeyAuth]
        [HttpGet]
        public string Get() => "Result";


    }
}


