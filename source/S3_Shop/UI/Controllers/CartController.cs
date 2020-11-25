using Model;
using Model.Common;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using UI.Helpers;
using UI.Models;

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

        #region Cart
        public List<CartItem> GetItemInCart()
        {
            var lst = Session[Constants.CART_SESSION] as List<CartItem>;
            if (lst == null)
            {
                lst = new List<CartItem>();
                Session[Constants.CART_SESSION] = lst;
            }
            return lst;
        }
        public ActionResult Index()
        {
            var cart = GetItemInCart();
            // cart = Session[Constants.CART_SESSION] as List<CartItem>;
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
                ViewBag.TongTien = TongTien(list).ToString("N0");
                ViewBag.TongSL = TongSL(list).ToString();
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
                    sum += item.Quantity * item.Product.Price;
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
            if (ModelState.IsValid)
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
                    item.Quantity++;
                return RedirectToAction("/");
            }
            else
                return RedirectToAction("/");
                //return View();
        }
        public ActionResult DeleteItem(int productID)
        {
            if (ModelState.IsValid)
            {
                List<CartItem> lst = GetItemInCart();
                CartItem sp = lst.SingleOrDefault(n => n.Product.ProductID == productID);
                if (sp != null)
                {
                    lst.RemoveAll(n => n.Product.ProductID == productID);
                    return View("Index");
                }
                if (lst.Count == 0)
                    return View("Index");
                return View("Index");
            }
            else
                return View();
        }
        public ActionResult UpdateItem(int productID,FormCollection c)
        {
            if (ModelState.IsValid)
            {
                List<CartItem> lst = GetItemInCart();
                CartItem sp = lst.SingleOrDefault(n => n.Product.ProductID == productID);
                sp.Quantity = int.Parse(c["txtSl"].ToString());
                return RedirectToAction("Index", "Cart");
            }
            else 
                return RedirectToAction("/");
        }
        #endregion
        public int TongTien(List<CartItem> lst)
        {
            int result = 0;
            foreach(var item in lst)
                result += item.Quantity * item.Product.Price;
            return result;
        }
        public int TongSL(List<CartItem> lst)
        {
            int result = 0;
            foreach (var item in lst)
                result += item.Quantity;
            return result;
        }
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
        #endregion

        #region Order
        public ActionResult Order()
        {
            if (Session[Constants.USER_SESSION] == null)
                return RedirectToAction("Login", "User");
            if (Session[Constants.CART_SESSION] == null)
                return RedirectToAction("Index", "Home");
            List<CartItem> lst = GetItemInCart();
            ViewBag.TongSL = TongSL(lst);
            ViewBag.TongTien = TongTien(lst);
            return View(lst);
        }
        [HttpPost]
        public ActionResult Order(string id)
        {
            //HOA_DON hd = new HOA_DON();
            //KHACH_HANG kh = (KHACH_HANG)Session["TaiKhoan"];
            List<CartItem> listCart = GetItemInCart();
            return View(listCart);
            //hd.MaHD = "HD" + data.HOA_DONs.Count();
            //hd.TaiKhoan = kh.TaiKhoan;
            //hd.ThoiGian = DateTime.Now;
            //var NgayGiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);
            //dh.NGAYGIAO = DateTime.Parse(NgayGiao);
            //hd.NGAYGIAO = DateTime.Now.AddDays(2);
            //hd.TINHTRANGGIAOHANG = "Đang tiếp nhận";
            // hd.TINHTRANGTHANHTOAN = "Chưa thanh toán";
            //data.HOA_DONs.InsertOnSubmit(hd);
            //data.SubmitChanges();

            //foreach (var item in gh)
            //{
            //    CHI_TIET_HOA_DON CTDH = new CHI_TIET_HOA_DON();
            //    CTDH.MaHD = hd.MaHD;
            //    CTDH.MaSP = Convert.ToInt32(item.ma);
            //    CTDH.SoLuong = item.sl;
            //    //Chú ý chỗ này chưa sửa donGia
            //    CTDH.GiaBan = (decimal)item.donGia;
            //    data.CHI_TIET_HOA_DONs.InsertOnSubmit(CTDH);
            //}
            //data.SubmitChanges();
            //Session["Giohang"] = null;
            //return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        #endregion
    }
}