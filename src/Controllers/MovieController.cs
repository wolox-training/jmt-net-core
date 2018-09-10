using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using TrainingNet.Models.DataBase;
using TrainingNet.Models.Views;
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
<<<<<<< a9abf0746710d784a1cc5ab977af6341f559832a

        [HttpGet("Add")]
        public IActionResult Add()
=======
        [HttpGet("AddMovie")]
        public IActionResult AddMovie()
>>>>>>> added editMovieView
        {
            return View(new MovieViewModel());
        }
<<<<<<< a9abf0746710d784a1cc5ab977af6341f559832a

        [HttpPost("Add")]
        public IActionResult Add(Movie movie){
=======
        [HttpPost("AddMovie")]
        public IActionResult AddMovie(Movie movie){
>>>>>>> added editMovieView
            UnitOfWork.Movies.Add(movie);
            UnitOfWork.Complete();
            return RedirectToAction("AddMovie");
        }
        [HttpGet("EditMovie/{id?}")]
        public IActionResult EditMovie(int id)
        {
            try{
                if(id == 0)
                    throw new NullReferenceException("The movie was not found");
                Movie movie = UnitOfWork.Movies.Get(id);
                var movieViewModel = new MovieViewModel(movie);
                return View(movieViewModel);
            }
            catch(NullReferenceException n){
                return NotFound();
            }
        }
        [HttpPost("EditMovie/{id?}")]
        public IActionResult EditMovie(MovieViewModel movie, int id){
            try{
                if(id == 0)
                    throw new NullReferenceException("The movie was not found");
                Movie movieToBeChanged = UnitOfWork.Movies.Get(id);
                movieToBeChanged.Update(movie);
                UnitOfWork.Movies.Update(movieToBeChanged);
                UnitOfWork.Complete();
                return RedirectToAction("EditMovie");
            }
            catch(NullReferenceException n){
                return NotFound();
            }
        }

        private IUnitOfWork UnitOfWork
        {
           get { return this._unitOfWork; }
        }
    }
}
