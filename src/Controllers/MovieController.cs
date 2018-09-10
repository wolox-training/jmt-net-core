using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult Add(MovieViewModel movieViewModel)
        {
            UnitOfWork.MovieRepository.Add(new Movie(movieViewModel));
            UnitOfWork.Complete();
            return RedirectToAction("ListMovies");
        }

        [HttpGet("EditMovie/{id}")]
        public IActionResult EditMovie(int id)
        {
            try
            {
                if (id == 0)
                    throw new NullReferenceException("The movie was not found");
                Movie movie = UnitOfWork.MovieRepository.Get(id);
                var movieViewModel = new MovieViewModel(movie);
                return View(movieViewModel);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpPost("EditMovie/{id}")]
        public IActionResult EditMovie(MovieViewModel movie, int id)
        {
            try
            {
                if (id == 0)
                    throw new NullReferenceException("The movie was not found");
                Movie movieToBeChanged = UnitOfWork.MovieRepository.Get(id);
                movieToBeChanged.Update(movie);
                UnitOfWork.MovieRepository.Update(movieToBeChanged);
                UnitOfWork.Complete();
                return RedirectToAction("ListMovies");
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpGet("")]
        [HttpGet("ListMovies/{searchString?}")]
        public IActionResult ListMovies(string titleSearchString, string genreSearchString)
        {
            var movieList = UnitOfWork.MovieRepository.GetAll().Select(s => new MovieViewModel(s));
            if (!String.IsNullOrEmpty(titleSearchString))
                movieList = movieList.Where(s => s.Title.Contains(titleSearchString));
            if (!String.IsNullOrEmpty(genreSearchString))
                movieList = movieList.Where(s => s.Genre.Contains(genreSearchString));
            return View(movieList);
        }

        [HttpGet("DeleteMovie/{id}")]
        public IActionResult DeleteMovie(int id)
        {
            //el try catch está por las dudas porque no sé lo que pasa cuando trato
            //de eliminar/gettear una película que no existe. Recordar preguntarlo.
            try
            {
                if (id == 0)
                    throw new NullReferenceException("The movie was not found");
                Movie movie = UnitOfWork.MovieRepository.Get(id);
                UnitOfWork.MovieRepository.Remove(movie);
                UnitOfWork.Complete();
                return RedirectToAction("ListMovies");
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpGet("ListMovies")]
        public IActionResult ListMovies(){
            IEnumerable movieList = UnitOfWork.Movies.GetAll();
            return View(movieList);
        }
        private IUnitOfWork UnitOfWork
        {
            get { return this._unitOfWork; }
        }
    }
}
