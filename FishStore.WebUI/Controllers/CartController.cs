using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FishStore.Domain.Entities;
using FishStore.Domain.Abstract;
using FishStore.WebUI.Models;

namespace FishStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IFishRepository repository;
        private IOrderProcessor orderProcessor;
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        public CartController(IFishRepository repo, IOrderProcessor processor)
        {
            repository = repo;
            orderProcessor = processor;
        }
        public CartController(IFishRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int FishId, string returnUrl)
        {
            Fish Fish = repository.Fishes
                .FirstOrDefault(g => g.FishId == FishId);

            if (Fish != null)
            {
                cart.AddItem(Fish, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int FishId, string returnUrl)
        {
            Fish Fish = repository.Fishes
                .FirstOrDefault(g => g.FishId == FishId);

            if (Fish != null)
            {
                cart.RemoveLine(Fish);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}