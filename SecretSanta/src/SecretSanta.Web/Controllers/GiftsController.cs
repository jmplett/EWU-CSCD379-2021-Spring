using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Controllers
{
    public class GiftsController : Controller
    { 

        static List<GiftViewModel> Gifts = new List<GiftViewModel>
        {
            new GiftViewModel {GiftName = "Gift1", GiftDescription = "It's a gift", GiftURL="www.Amazon.com", GiftPriority=1, GiftUser="Justin"},
        };

        public IActionResult Index()
        {
            return View(Gifts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(GiftViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Gifts.Add(viewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            Gifts[id].Id = id;
            return View(Gifts[id]);
        }

        [HttpPost]

        public IActionResult Edit(GiftViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Gifts[viewModel.Id] = viewModel;
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        [HttpPost]

        public IActionResult Delete(int id)
        {
            Gifts.RemoveAt(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
