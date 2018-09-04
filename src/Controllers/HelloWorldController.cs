using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace TrainingNet.HelloWorldController
{
    [Route("[controller]")]
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/

        [HttpGet("")]
        [HttpGet("Index")]
        public string Index()
        {
            return "This is my default action...";
        }

        // 
        // GET: /HelloWorld/Welcome/ 

        [HttpGet("Welcome/{id?}")]
        public string Welcome(string name, int ID = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, ID is {ID}");
        }
    }
}