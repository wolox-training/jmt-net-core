using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using System.Text.Encodings.Web;

namespace TrainingNet.Controllers
{
    [Route("[controller]")]
    public class HelloWorldController : Controller
    {
<<<<<<< b3b1502e624c9bf1eac63fdf4a9ff9f2c726bf36
        private readonly IHtmlLocalizer<HomeController> _localizer;
<<<<<<< f2b2082a89ceb8ede60083ff0d05aca34bca99c6
        
=======

>>>>>>> fixed PR requests, changed name from AddMovie to Add
        public HelloWorldController(IHtmlLocalizer<HomeController> localizer)
        {
            this._localizer = localizer;
        }

=======

        private readonly IHtmlLocalizer<HomeController> _localizer;

        public HelloWorldController(IHtmlLocalizer<HomeController> localizer){
            this._localizer = localizer;
        }
        // 
        // GET: /HelloWorld/
>>>>>>> finished Repository pattern card
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
<<<<<<< b3b1502e624c9bf1eac63fdf4a9ff9f2c726bf36
            ViewData["Message"] = Localizer["Greeting"].Value + name;
=======
            ViewData["Message"] = _localizer["Greeting"].Value + name;
>>>>>>> finished Repository pattern card
            ViewData["NumTimes"] = numTimes;
            return View();
        }

        private IHtmlLocalizer Localizer
        {
            get { return this._localizer; }
        }
    }
}
