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
using TrainingNet.Paging;
using System.Threading.Tasks;

namespace TrainingNet.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private const int DEFAULT_STARTING_PAGE = 0;
        private const int DEFAULT_PAGE_SIZE = 5;
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
        public IActionResult ListMovies(string titleSearchString, string genreSearchString,
                                        string currentTitleFilter, string currentGenreFilter,
                                        int page = DEFAULT_STARTING_PAGE, int pageSize = DEFAULT_PAGE_SIZE,
                                        string sortOrder = "title", bool descending = false)
        {
            ViewData["CurrentSort"] = sortOrder;
            if (!String.IsNullOrEmpty(titleSearchString) || !String.IsNullOrEmpty(genreSearchString))
                page = DEFAULT_STARTING_PAGE;
            else if (String.IsNullOrEmpty(titleSearchString) && String.IsNullOrEmpty(genreSearchString))
            {
                titleSearchString = currentTitleFilter;
                genreSearchString = currentGenreFilter;
            }
            else if (String.IsNullOrEmpty(genreSearchString))
                genreSearchString = currentGenreFilter;
            else
                titleSearchString = currentTitleFilter;
            genreSearchString = currentGenreFilter;
            var movieList = UnitOfWork.MovieRepository.GetAll().Select(s => new MovieViewModel(s));
            movieList = getFilteredMovies(titleSearchString, genreSearchString, movieList).AsQueryable();
            movieList = getSortedMovies(sortOrder, movieList, descending).AsQueryable();
            MovieGenreViewModel movieGenreViewModel = new MovieGenreViewModel();
            movieGenreViewModel.Movies = PaginatedList<MovieViewModel>.Create(movieList.AsQueryable(), page, pageSize);
            movieGenreViewModel.Genres = new SelectList(UnitOfWork.MovieRepository.GetGenres().ToList());
            ViewData["descending"] = descending;
            return View(movieGenreViewModel);
        }

        // Descending order by title is the default, hence why it is returned when the string is null, empty or unknown value.
        private IEnumerable<MovieViewModel> getSortedMovies(string sortOrder, IEnumerable<MovieViewModel> movieList, bool descending)
        {
            sortOrder = sortOrder.ToLower();
            switch (sortOrder)
            {
                case "title":
                    if(descending)
                        return movieList.OrderByDescending(s => s.Title);
                    else
                        return movieList.OrderBy(s => s.Title);
                case "price":
                    if(descending)
                        return movieList.OrderByDescending(s => s.Price);
                    else
                        return movieList.OrderBy(s => s.Price);
                case "genre":
                    if(descending)
                        return movieList.OrderByDescending(s => s.Genre);
                    else
                        return movieList.OrderBy(s => s.Genre);
                case "rating":
                    if(descending)
                        return movieList.OrderByDescending(s => s.Rating);
                    else
                        return movieList.OrderBy(s => s.Rating);
                case "release date":
                    if(descending)
                        return movieList.OrderByDescending(s => s.ReleaseDate);
                    else
                        return movieList.OrderBy(s => s.ReleaseDate);
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
