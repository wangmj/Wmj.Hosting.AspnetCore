using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wmj.Hosting.Sample.Areas.Admin.Controllers
{
    [Route("{area:exists}/api/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Here is Admin Area!";
        }
    }
}
