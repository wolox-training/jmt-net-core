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
        private readonly IUnitOfWork _unitOfWork;

        public MovieController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        
        [HttpGet("Add")]
        public IActionResult Add()
        {
            return View(new MovieViewModel());
        }

        [HttpPost("Add")]
        public IActionResult Add(Movie movie)
        {
            UnitOfWork.MovieRepository.Add(movie);
            UnitOfWork.MovieRepository.Add(movie);
            UnitOfWork.Complete();
            return RedirectToAction("AddMovie");
        }

        [HttpGet("EditMovie/{id?}")]
        public IActionResult EditMovie(int id)
        {
            try{
                if(id == 0)
                    throw new NullReferenceException("The movie was not found");
                Movie movie = UnitOfWork.MovieRepository.Get(id);
                var movieViewModel = new MovieViewModel(movie);
                return View(movieViewModel);
            }
            catch(NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpPost("EditMovie/{id?}")]
        public IActionResult EditMovie(MovieViewModel movie, int id)
        {
            try{
                if(id == 0)
                    throw new NullReferenceException("The movie was not found");
                Movie movieToBeChanged = UnitOfWork.MovieRepository.Get(id);
                movieToBeChanged.Update(movie);
                UnitOfWork.MovieRepository.Update(movieToBeChanged);
                UnitOfWork.Complete();
                return RedirectToAction("EditMovie");
            }
            catch(NullReferenceException)
            {
                return NotFound();
            }
        }

        private IUnitOfWork UnitOfWork
        {
           get { return this._unitOfWork; }
        }
    }
}
