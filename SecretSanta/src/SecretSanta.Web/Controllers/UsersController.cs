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

        public IActionResult Edit(int id)
        {
            Users[id].Id = id;
            return View(Users[id]);
        }

        [HttpPost]

        public IActionResult Edit(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Users[viewModel.Id] = viewModel;
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        [HttpPost]

        public IActionResult Delete(int id)
        {
            Users.RemoveAt(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
