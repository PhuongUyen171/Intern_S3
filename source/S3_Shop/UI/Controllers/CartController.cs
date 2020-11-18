using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class CartController : Controller
    {
        private const string CART_SESSION = "CART_SESSION";
        public ActionResult Index()
        {
            var cart = Session[CART_SESSION];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
                int tamTinh = 0;
                foreach (var item in list)
                {
                    tamTinh += item.Quantity * item.Product.Price;
                }
                ViewBag.TamTinh = tamTinh.ToString("N0");
                ViewBag.ThanhTien = tamTinh.ToString("N0");
                return View(list);
            }
            return View();
        }
    }
}