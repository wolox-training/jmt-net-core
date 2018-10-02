using System;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using TrainingNet.Models.DataBase;
using TrainingNet.Repositories.Interfaces;

namespace Controllers.api.v1
{
    [Route("api/v1/[controller]")]
    public class CommentApiController : Controller
    {
        IUnitOfWork _unitOfWork;

        public CommentApiController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpPost("AddComment/{id}")]
        public IActionResult AddComment(int id, string comment)
        {
            try
            {
                Movie movieToBeChanged = UnitOfWork.MovieRepository.GetMovieWithComments(id);
                if(movieToBeChanged == null)
                    throw new NullReferenceException("movie not found");
                movieToBeChanged.Comments.Add(new Comment(movieToBeChanged, comment));
                UnitOfWork.MovieRepository.Update(movieToBeChanged);
                UnitOfWork.Complete();
                return Json(new {Comment = comment, Message = "comment added successfully"});
            }
            catch (NullReferenceException)
            {
                Response.StatusCode = 404;
                return Json(new {Message = "comment not added"});
            }
        }

        IUnitOfWork UnitOfWork 
        {
            get { return this._unitOfWork; }
        }
    }
}