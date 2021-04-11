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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Users.Add(viewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }
    }
}
