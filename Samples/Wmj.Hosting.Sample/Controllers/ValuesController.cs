using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wmj.Hosting.Sample.Filters;

namespace Wmj.Hosting.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [ActionValidateFilter]
        public IActionResult Get(string q)
        {
            return Ok("Success");
        }
    }
}
