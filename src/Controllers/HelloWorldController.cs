using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using System.Text.Encodings.Web;

namespace TrainingNet.Controllers
{
    [Route("[controller]")]
    public class HelloWorldController : Controller
    {
        private readonly IHtmlLocalizer<HomeController> _localizer;
        public HelloWorldController(IHtmlLocalizer<HomeController> localizer){
            this._localizer = localizer;
        }
        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("Welcome/{id?}")]
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = _localizer["Greeting"].Value + name;
            ViewData["NumTimes"] = numTimes;
            return View();
        }
    }
}
