using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Model.Common;
using API.Models;
using UI.Models;

namespace UI.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            if (Session[Constants.USER_SESSION] == null )
                return RedirectToAction("Login", "Login");
            if (Session[Constants.CART_SESSION] == null)
                return RedirectToAction("Index", "Home");
            List<CartItem> lst = new CartController().GetItemInCart();
            //ViewBag.TongSL = c.TongSL(lst);
            //ViewBag.TongTien = c.TongTien(lst);
            return View(lst);
        }
        [HttpPost]
        public ActionResult Order()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CancelOrder()
        {
            return View();
        }
    }
}