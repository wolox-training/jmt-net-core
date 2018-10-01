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
            if (ModelState.IsValid)
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
                Movie movie = UnitOfWork.MovieRepository.Get(id);
                if (movie == null)
                    throw new NullReferenceException("The movie was not found");
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
                    Movie movieToBeChanged = UnitOfWork.MovieRepository.Get(id);
                    if(movieToBeChanged == null)
                        throw new NullReferenceException("The movie was not found");
                    movieToBeChanged.Update(movie);
                    UnitOfWork.MovieRepository.Update(movieToBeChanged);
                    UnitOfWork.Complete();
                    return RedirectToAction("ListMovies");
                }
                else
                    return View(movie);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpGet("")]
        [HttpGet("ListMovies")]
        public IActionResult ListMovies(string titleSearchString, string genreSearchString, string sortOrder)
        {
            var movieList = UnitOfWork.MovieRepository.GetAll().Select(s => new MovieViewModel(s));
            movieList = getFilteredMovies(titleSearchString, genreSearchString, movieList);
            movieList = getSortedMovies(sortOrder, movieList);
            MovieGenreViewModel movieGenreViewModel = new MovieGenreViewModel();
            movieGenreViewModel.Movies = movieList;
            movieGenreViewModel.Genres = new SelectList(UnitOfWork.MovieRepository.GetGenres().ToList());
            return View(movieList);
        }

        // Descending order by title is the default, hence why it is returned when the string is null, empty or unknown value.
        private IEnumerable<MovieViewModel> getSortedMovies(string sortOrder, IEnumerable<MovieViewModel> movieList)
        {
            if (String.IsNullOrEmpty(sortOrder))
                return movieList.OrderBy(s => s.Title);
            sortOrder = sortOrder.ToLower();
            switch (sortOrder)
            {
                case "title":
                    return movieList.OrderBy(s => s.Title);
                case "price":
                    return movieList.OrderByDescending(s => s.Price);
                case "genre":
                    return movieList.OrderBy(s => s.Genre);
                case "rating":
                    return movieList.OrderBy(s => s.Rating);
                default:
                    return movieList.OrderBy(s => s.Title);
            }
        }

        private IEnumerable<MovieViewModel> getFilteredMovies(string titleSearchString, string genreSearchString, IEnumerable<MovieViewModel> movieList)
        {
            if (!String.IsNullOrEmpty(titleSearchString))
            {
                titleSearchString = titleSearchString.ToLower();
                movieList = movieList.Where(s => s.Title.ToLower().Contains(titleSearchString));
            }
            if (!String.IsNullOrEmpty(genreSearchString))
                movieList = movieList.Where(s => s.Genre.Equals(genreSearchString));
            return movieList;
        }

        [HttpGet("DeleteMovie/{id}")]
        public IActionResult DeleteMovie(int id)
        {
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
                Movie movie = UnitOfWork.MovieRepository.Get(id);
                if (movie == null)
                    throw new NullReferenceException("the movie was not found");
                Mailer.Send(userEmail, "Movie details", movie.ToString());
                return RedirectToAction("ListMovies");
            }
            catch (NullReferenceException)
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
