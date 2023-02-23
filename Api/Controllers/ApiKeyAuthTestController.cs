using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiKeyAuthTestController : ControllerBase
    {
        private readonly ILogger<ApiKeyAuthTestController> _logger;

        public ApiKeyAuthTestController(ILogger<ApiKeyAuthTestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get() => "Result";


    }


}
