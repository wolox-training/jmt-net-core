using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using TrainingNet.Models.DataBase;
using TrainingNet.Models.Views;
using TrainingNet.Repositories;
using TrainingNet.Repositories.Database;
using TrainingNet.Repositories.Interfaces;

namespace NetCoreBootstrap.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class UserManagementController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserManagementController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork
        {
            get { return this._unitOfWork; }
        }

        [HttpGet("Users")]
        public IActionResult Users()
        {
            return View(UnitOfWork.UserRepository.GetAll().Select(s => new ApplicationUserViewModel(){UserName = s.UserName}));
        }
    }
}
