using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

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
                db.BILLINFOes.Add(billInfo);
                db.SaveChanges();
                return true;
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
        public List<BILLINFO> GetAllBillInfoByID(int id)
        {
            return db.BILLINFOes.Where(t => t.BillID == id).ToList();
        }
        #endregion
        public BILLINFO GetBillInfoByBillInforID(int billID, int productID)
        {
            return db.BILLINFOes.FirstOrDefault(t => t.BillID == billID & t.ProductID == productID);
        }
        
    }
}
