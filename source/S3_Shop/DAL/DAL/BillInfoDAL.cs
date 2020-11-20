using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using Model.Common;

namespace DAL.DAL
{
    public class BillInfoDAL
    {
        private S3ShopDbContext db = new S3ShopDbContext();
        public BillInfoDAL()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        #region CRUD
        public bool InsertBillInfo(BILLINFO billInfo)
        {
            try
            {
                //Hết hàng
                var product = db.PRODUCTs.Find(billInfo.ProductID);
                if (product.Quantity < billInfo.Quantity)
                    return false;
                List<BILLINFO> cart = db.BILLINFOes.Where(t => t.BillID == billInfo.BillID).ToList(); 
                if (cart != null)//Có bill rồi
                {
                    //Giỏ hàng có sp thì cộng dồn
                    if (cart.Exists(x=>x.ProductID==billInfo.ProductID))
                    {
                        foreach (var item in cart)
                            if (item.ProductID == billInfo.ProductID)
                                item.Quantity += billInfo.Quantity;
                    }
                    else //Giỏ hàng chưa có thì thêm mới
                    {
                        var item = new BILLINFO
                        {
                            BillID = billInfo.BillID,
                            ProductID = billInfo.ProductID,
                            Price = billInfo.Price,
                            Quantity = billInfo.Quantity
                        };
                        db.BILLINFOes.Add(item);
                        db.SaveChanges();
                    }
                }
                //else
                //{

                //}
                return true;
                /*
        var product = db.Saches.Find(id);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Sach.SachID == id))
                {

                    foreach (var item in list)
                    {
                        if (item.Sach.SachID == id)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.Sach = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gán vào session
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.Sach = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
                 */
                //db.BILLINFOes.Add(billInfo);
                //db.SaveChanges();
                //return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteBillInfo(int billID,int productId)
        {
            try
            {
                var itemDelete = GetBillInfoByBillInforID(billID, productId);
                if (itemDelete != null)
                {
                    db.BILLINFOes.Remove(itemDelete);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateBillInfo(BILLINFO billInfo)
        {
            try
            {
                var itemUpdate = GetBillInfoByBillInforID(billInfo.BillID, billInfo.ProductID);
                if (itemUpdate != null)
                {
                    itemUpdate.Quantity = billInfo.Quantity;
                    itemUpdate.Price = billInfo.Price;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Get Information
        public List<BILLINFO> GetAllBillInfoByID(int id)
        {
            return db.BILLINFOes.Where(t => t.BillID == id).ToList();
        }
        public BILLINFO GetBillInfoByBillInforID(int billID, int productID)
        {
            return db.BILLINFOes.FirstOrDefault(t => t.BillID == billID & t.ProductID == productID);
        }
        #endregion
    }
}
