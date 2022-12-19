using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wmj.Hosting.Sample.Filters;

namespace Wmj.Hosting.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        ILogger<ValuesController> logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            this.logger = logger;
        }

        //[ActionValidateFilter]
        public IActionResult Get(string q)
        {
            logger.LogInformation("ValuesController-get");
            return Ok("Success");
        }
    }
}
