using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

namespace DAL.DAL
{
    public class GroupAdminDAL
    {
        private S3ShopDbContext db = new S3ShopDbContext();
        public GroupAdminDAL()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        #region CRUD
        public List<GROUPADMIN> GetAllGroupAdmins()
        {
            return db.GROUPADMINs.ToList();
        }
        public bool InsertGroupAdmin(GROUPADMIN group) {
            try
            {
                db.GROUPADMINs.Add(group);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteGroupAdmin(string id)
        {
            try
            {
                var itemDelete = GetGroupAdminByID(id);
                if (itemDelete != null)
                {
                    db.GROUPADMINs.Remove(itemDelete);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateGroupAdmin(GROUPADMIN group)
        {
            try
            {
                var itemUpdate = GetGroupAdminByID(group.GroupID);
                if (itemUpdate != null)
                {
                    itemUpdate.GroupName = group.GroupName;
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
        public GROUPADMIN GetGroupAdminByID(string id)
        {
            return db.GROUPADMINs.FirstOrDefault(t => t.GroupID == id);
        }
        public int GetEmployeeByGroupID(string id)
        {
            return db.EMPLOYEEs.Count(t => t.GroupID == id);
        }
    }
}
