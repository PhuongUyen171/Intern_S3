using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
namespace DAL.DAL
{
    public class PermisionDAL
    {
        private S3ShopDbContext db;
        public PermisionDAL()
        {
            db = new S3ShopDbContext();
            db.Configuration.ProxyCreationEnabled = false;
        }
        #region CRUD
        public List<PERMISION> GetAllPermisions()
        {
            return db.PERMISIONs.ToList();
        }
        public bool InsertPermision(PERMISION per)
        {
            try
            {
                db.PERMISIONs.Add(per);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdatePermision(PERMISION per)
        {
            try
            {
                var itemUpdate = GetPermisionByID(per.GroupID, per.RoleID);
                if (itemUpdate != null)
                {
                    itemUpdate.PerID = per.PerID;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeletePermision(string groupID, int roleID)
        {
            try
            {
                var itemDelete = GetPermisionByID(groupID,roleID);
                if (itemDelete != null)
                {
                    db.PERMISIONs.Remove(itemDelete);
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
        public PERMISION GetPermisionByID(string groupId, int roleID)
        {
            return db.PERMISIONs.FirstOrDefault(t => t.GroupID == groupId & t.RoleID == roleID);
        }
    }
}
