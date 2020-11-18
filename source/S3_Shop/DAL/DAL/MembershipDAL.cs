using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

namespace DAL.DAL
{
    public class MembershipDAL
    {
        private S3ShopDbContext db = new S3ShopDbContext();
        public MembershipDAL()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        #region CRUD
        public List<MEMBERSHIP> GetAllMembership()
        {
            return db.MEMBERSHIPs.ToList();
        }
        public bool InsertMembership(MEMBERSHIP membership)
        {
            try
            {
                db.MEMBERSHIPs.Add(membership);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteMembership(string id)
        {
            try
            {
                var itemDelete = GetMembershipByID(id);
                if (itemDelete != null)
                {
                    db.MEMBERSHIPs.Remove(itemDelete);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateMembership(MEMBERSHIP membership)
        {
            try
            {
                var itemUpdate = GetMembershipByID(membership.MemID);
                if (itemUpdate != null)
                {
                    itemUpdate.MemName = membership.MemName;
                    itemUpdate.MinPrice = membership.MinPrice;
                    itemUpdate.MaxPrice = membership.MaxPrice;
                    itemUpdate.Sale = membership.Sale;
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
        public MEMBERSHIP GetMembershipByID(string id)
        {
            return db.MEMBERSHIPs.FirstOrDefault(t => t.MemID == id);
        }
        public int GetCustomerByMemID(string id)
        {
            return db.CUSTOMERs.Count(t => t.MemID == id);
        }
    }
}
