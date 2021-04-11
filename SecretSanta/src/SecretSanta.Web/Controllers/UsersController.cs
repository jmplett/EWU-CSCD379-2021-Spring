using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Controllers
{
    public class UsersController : Controller
    {
        static List<UserViewModel> Users = new List<UserViewModel>
        {
            new UserViewModel {FirstName = "Justin", LastName = "Plett"},
            new UserViewModel {FirstName = "Jimmy", LastName = "Jo"},
        };

        public IActionResult Index()
        {
            return View(Users);
        }
    }
}
