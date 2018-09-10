using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using TrainingNet.Models.DataBase;
using TrainingNet.Repositories;
using TrainingNet.Repositories.Database;
using TrainingNet.Repositories.Interfaces;

namespace TrainingNet.Controllers
{
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private readonly IHtmlLocalizer<HomeController> _localizer;
        private readonly IUnitOfWork _unitOfWork;
        
        public MovieController(IHtmlLocalizer<HomeController> localizer, IUnitOfWork unitOfWork){
            this._localizer = localizer;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet("Add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("Add")]
        public IActionResult Add(Movie movie){
            UnitOfWork.Movies.Add(movie);
            UnitOfWork.Complete();
            return RedirectToAction("Index");
        }

        private IUnitOfWork UnitOfWork
        {
           get { return this._unitOfWork; }
        }
    }
}
