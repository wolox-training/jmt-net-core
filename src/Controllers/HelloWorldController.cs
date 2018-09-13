using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using System.Text.Encodings.Web;

namespace TrainingNet.Controllers
{
    [Route("[controller]")]
    public class HelloWorldController : Controller
    {
        private readonly IHtmlLocalizer<HomeController> _localizer;
<<<<<<< f2b2082a89ceb8ede60083ff0d05aca34bca99c6
        
=======

>>>>>>> fixed PR requests, changed name from AddMovie to Add
        public HelloWorldController(IHtmlLocalizer<HomeController> localizer)
        {
            this._localizer = localizer;
        }

        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
<<<<<<< f2b2082a89ceb8ede60083ff0d05aca34bca99c6
        
=======

>>>>>>> fixed PR requests, changed name from AddMovie to Add
        [HttpGet("Welcome/{id?}")]
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = Localizer["Greeting"].Value + name;
            ViewData["NumTimes"] = numTimes;
            return View();
        }

        private IHtmlLocalizer Localizer
        {
            get { return this._localizer; }
        }
    }
}
