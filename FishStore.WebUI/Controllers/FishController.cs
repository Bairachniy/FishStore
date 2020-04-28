using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FishStore.Domain.Abstract;
using FishStore.Domain.Entities;
using FishStore.WebUI.Models;

namespace FishStore.WebUI.Controllers
{
    public class FishController : Controller
    {
        // GET: Fish
        private IFishRepository repository;
        public int pageSize = 4;
        public FishController(IFishRepository fishRepository)
        {
            repository = fishRepository;
        }
        public ViewResult List(string category, int page = 1)
        {
            FishesListViewModel model = new FishesListViewModel
            {
                Fishes = repository.Fishes
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(fish => fish.FishId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
        repository.Fishes.Count() :
        repository.Fishes.Where(fish => fish.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public FileContentResult GetImage(int fishId)
        {
            Fish fish = repository.Fishes
                .FirstOrDefault(g => g.FishId == fishId);

            if (fish != null)
            {
                return File(fish.ImageData, fish.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}