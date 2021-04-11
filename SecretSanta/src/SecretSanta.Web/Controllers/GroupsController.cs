using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Controllers
{
    public class GroupsController : Controller
    { 

        static List<GroupViewModel> Groups = new List<GroupViewModel>
        {
            new GroupViewModel {GroupName = "Group1"},
            new GroupViewModel {GroupName = "Group2"},
        };

        public IActionResult Index()
        {
            return View(Groups);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(GroupViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Groups.Add(viewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            Groups[id].Id = id;
            return View(Groups[id]);
        }

        [HttpPost]

        public IActionResult Edit(GroupViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Groups[viewModel.Id] = viewModel;
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        [HttpPost]

        public IActionResult Delete(int id)
        {
            Groups.RemoveAt(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
