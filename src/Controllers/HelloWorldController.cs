using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace TrainingNet.Controllers
{
    [Route("[controller]")]
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/

        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        // 
        // GET: /HelloWorld/Welcome/ 

        [HttpGet("Welcome/{id?}")]
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}