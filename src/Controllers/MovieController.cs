using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingNet.Mail;
using TrainingNet.Models.DataBase;
using TrainingNet.Models.Views;
using TrainingNet.Repositories;
using TrainingNet.Repositories.Database;
using TrainingNet.Repositories.Interfaces;

namespace TrainingNet.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private readonly IHtmlLocalizer<HomeController> _localizer;
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
            if(ModelState.IsValid)
            {
                UnitOfWork.MovieRepository.Add(new Movie(movieViewModel));
                UnitOfWork.Complete();
                return RedirectToAction("ListMovies");
            }
            return View(movieViewModel);
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
                if(ModelState.IsValid)
                {
                    if (id == 0)
                        throw new NullReferenceException("The movie was not found");
                    Movie movieToBeChanged = UnitOfWork.MovieRepository.Get(id);
                    movieToBeChanged.Update(movie);
                    UnitOfWork.MovieRepository.Update(movieToBeChanged);
                    UnitOfWork.Complete();
                    return RedirectToAction("ListMovies");
                }
                return View(movie);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpGet("ListMovies")]
        public IActionResult ListMovies(string titleSearchString, string genreSearchString)
        {
            var movieList = UnitOfWork.MovieRepository.GetAll().Select(s => new MovieViewModel(s));
            if (!String.IsNullOrEmpty(titleSearchString))
            {
                titleSearchString = titleSearchString.ToLower();
                movieList = movieList.Where(s => s.Title.ToLower().Contains(titleSearchString));
            }
            if (!String.IsNullOrEmpty(genreSearchString))
                movieList = movieList.Where(s => s.Genre.Equals(genreSearchString));
            MovieGenreViewModel movieGenreViewModel = new MovieGenreViewModel();
            movieGenreViewModel.Movies = movieList;
            movieGenreViewModel.Genres = new SelectList(UnitOfWork.MovieRepository.GetGenres().ToList());
            return View(movieGenreViewModel);
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

        [HttpPost("Email/{id}")]
        public IActionResult Email(int id, string userEmail)
        {

            try
            {
                if (id == 0)
                    throw new NullReferenceException("The movie was not found");
                Movie movie = UnitOfWork.MovieRepository.Get(id);
                Mailer.Send(userEmail, "Movie details", movie.ToString());
                return RedirectToAction("ListMovies");
            }
            catch (NullReferenceException n)
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
