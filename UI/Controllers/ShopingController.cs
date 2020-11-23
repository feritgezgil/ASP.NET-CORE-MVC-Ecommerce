using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UI.Models;
using UI.Models.DAL;
using UI.Models.UI;

namespace UI.Controllers
{
    [Authorize]
    public class ShopingController : Controller
    {
        private readonly ECommerceContext _context;
        private CartModel cartModel = new CartModel();
        public ShopingController(ECommerceContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public MiniCartModel MiniCart => new MiniCartModel();

        public void PurchaseCart()
        {
            CartModel c = new CartModel();
            c.PurchaseCart();
            RedirectToAction("Index", "Home");
        }

        public void DeleteCart()
        {
            CartModel c = new CartModel();
            c.DeleteCart();
            RedirectToAction("Index", "Home");
        }

        public IActionResult CartDetail()
        {
            return View(cartModel.CreateCart());
        }

 }
}
