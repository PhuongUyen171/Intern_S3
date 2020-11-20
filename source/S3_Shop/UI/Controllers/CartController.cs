using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models;
using System.Web.Script.Serialization;
using Model;
using API.Models;
using System.Net.Http;

namespace UI.Controllers
{
    public class CartController : Controller
    {
        private string url;
        private ServiceRepository serviceObj;
        public CartController()
        {
            serviceObj = new ServiceRepository();
            url = "https://localhost:44379/";
        }
        public List<CartItem> GetItemInCart()
        {
            List<CartItem> lst = Session[Constants.CART_SESSION] as List<CartItem>;
            if (lst == null)
            {
                lst = new List<CartItem>();
                Session[Constants.CART_SESSION] = lst;
            }
            return lst;
        }

        public ActionResult Index()
        {
            var cart = Session[Constants.CART_SESSION] as List<CartItem>;
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
                int tamTinh = 0;
                int sl = 0;
                foreach (var item in list)
                {
                    tamTinh += item.Quantity * item.Product.Price;
                    sl += item.Quantity;
                }
                ViewBag.TamTinh = tamTinh.ToString("N0");
                ViewBag.ThanhTien = tamTinh.ToString("N0");
                ViewBag.TongTien = tamTinh.ToString("N0");
                ViewBag.TongSL = sl + "";
                return View(list);
            }
            return View();
        }
        public ActionResult CartPartial()
        {
            var cart = Session[Constants.CART_SESSION];
            var list = new List<CartItem>();
           int sum = 0;
            if (cart != null)
            {
                list = (List<CartItem>)cart;
                foreach (var item in list)
                {
                    sum += item.Quantity * item.Product.Price;
                }
                ViewBag.ThanhTien = sum.ToString("N0");
            }
            return PartialView(list);
        }
        public ActionResult CartIcon()
        {
            var cart = Session[Constants.CART_SESSION];
            var list = new List<CartItem>();
            int sum = 0;
            if (cart != null)
            {
                list = (List<CartItem>)cart;
                foreach (var item in list)
                    sum += item.Quantity;
            }
            ViewBag.NumCart = sum;
            return PartialView();
        }

        #region Insert, Update, Delete
        public ActionResult AddItem(int productID, int quantity)
        {
            if(ModelState.IsValid)
            {
                var cartList = GetItemInCart();
                CartItem item = cartList.Find(x => x.Product.ProductID == productID);

                if (item == null)
                {
                    ProductModel model = GetProductByID(productID);
                    item = new CartItem(model, quantity);
                    cartList.Add(item);
                }
                else
                {
                    item.Quantity++;
                }
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Index");
        }
        public ActionResult DeleteItemCart(int productID)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            else
                return View();
        }
        #endregion

        public ProductModel GetPriceProductByID(int id)
        {
            HttpResponseMessage response = serviceObj.GetResponse(url + "api/Product_API/GetPriceProductByID?id=" + id);
            response.EnsureSuccessStatusCode();
            ProductModel pro = response.Content.ReadAsAsync<ProductModel>().Result;
            return pro;
        }
        public ProductModel GetProductByID(int id)
        {
            HttpResponseMessage response = serviceObj.GetResponse(url + "api/Product_API/GetProductByID?id=" + id);
            response.EnsureSuccessStatusCode();
            ProductModel pro = response.Content.ReadAsAsync<ProductModel>().Result;
            return pro;
        }
    }
}