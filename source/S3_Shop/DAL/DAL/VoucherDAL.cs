using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

namespace DAL.DAL
{
    public class VoucherDAL
    {
        private S3ShopDbContext db = new S3ShopDbContext();
        public VoucherDAL()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        #region CRUD
        public List<VOUCHER> GetAllVouchers()
        {
            return db.VOUCHERs.ToList();
        }
        public bool InsertVoucher(VOUCHER voucher)
        {
            try
            {
                db.VOUCHERs.Add(voucher);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateVoucher(VOUCHER voucher)
        {
            try
            {
                var itemUpdate = GetVoucherByID(voucher.VoucherID);
                if (itemUpdate != null)
                {
                    itemUpdate.Sale = voucher.Sale;
                    itemUpdate.Images = voucher.Images;
                    itemUpdate.Title = voucher.Title;
                    itemUpdate.EndDate = voucher.EndDate;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteVoucher(string id)
        {
            try
            {
                var itemDelete = GetVoucherByID(id);
                if (itemDelete != null)
                {
                    db.VOUCHERs.Remove(itemDelete);
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
        public VOUCHER GetVoucherByID(string id)
        {
            return db.VOUCHERs.FirstOrDefault(t=>t.VoucherID==id);
        }
    }
}
