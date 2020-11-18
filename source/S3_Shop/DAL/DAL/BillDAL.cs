using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

namespace DAL.DAL
{
    public class BillDAL
    {
        private S3ShopDbContext db = new S3ShopDbContext();
        public BillDAL()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        #region CRUD
        public List<BILL> GetAllBills()
        {
            return db.BILLs.ToList();
        }
        public bool InsertBill(BILL bill)
        {
            try
            {
                db.BILLs.Add(bill);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateBill(BILL bill)
        {
            try
            {
                var itemUpdate = GetBillByID(bill.BillID);
                if (itemUpdate != null)
                {
                    itemUpdate.CustomID = bill.CustomID;
                    itemUpdate.DeliveryDate = bill.DeliveryDate;
                    itemUpdate.EmployID = bill.EmployID;
                    itemUpdate.PublishDate = bill.PublishDate;
                    itemUpdate.ToTalPrice = bill.ToTalPrice;
                    itemUpdate.Sale = bill.Sale;
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
        public BILL GetBillByID(int id)
        {
            return db.BILLs.FirstOrDefault(t => t.BillID == id);
        }
        public List<BILLINFO> GetBillInfoByBillID(int id)
        {
            return db.BILLINFOes.Where(t => t.BillID == id).ToList();
        }
        public int? GetTotalBillByYear(DateTime date)
        {
            var total = (from t in db.BILLs
                         where t.PublishDate.Value.Year == date.Year 
                         select t).Sum(x => x.ToTalPrice);
            return (total != null) ? total : 0;
        }
        public int? GetTotalPriceByBillInfo(int id)
        {
            var total = (from t in db.BILLINFOes
                         where t.BillID == id
                         select t).Sum(x => x.Price * x.Price);
            return (total != null) ? total : 0;
        }
    }
}
