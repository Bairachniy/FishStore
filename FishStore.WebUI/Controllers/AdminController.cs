using FishStore.Domain.Abstract;
using FishStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FishStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IFishRepository repository;

        public AdminController(IFishRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Fishes);
        }
        public ViewResult Edit(int fishId)
        {
            Fish fish = repository.Fishes
                .FirstOrDefault(g => g.FishId == fishId);
            return View(fish);
        }

        // Перегруженная версия Edit() для сохранения изменений
        //[HttpPost]
        //public ActionResult Edit(Fish fish)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        repository.SaveFish(fish);
        //        TempData["message"] = string.Format("Изменения в товаре \"{0}\" были сохранены", fish.Name);
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        // Что-то не так со значениями данных
        //        return View(fish);
        //    }
        //}
        public ViewResult Create()
        {
            return View("Edit", new Fish());
        }
        [HttpPost]
        public ActionResult Delete(int fishId)
        {
            Fish deletedFish = repository.DeleteFish(fishId);
            if (deletedFish != null)
            {
                TempData["message"] = string.Format("Товар \"{0}\" был удалён",
                    deletedFish.Name);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Fish fish, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    fish.ImageMimeType = image.ContentType;
                    fish.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(fish.ImageData, 0, image.ContentLength);
                }
                repository.SaveFish(fish);
                TempData["message"] = string.Format("Изменения в игре \"{0}\" были сохранены", fish.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(fish);
            }
        }
    }
}