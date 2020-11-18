using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

namespace DAL.DAL
{
    public class StoreDAL
    {
        private S3ShopDbContext db = new S3ShopDbContext();
        public StoreDAL()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        #region CRUD
        public bool InsertStore(STORE store)
        {
            try
            {
                db.STOREs.Add(store);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateStore(STORE store)
        {
            try
            {
                var itemUpdate = GetStoryByID(store.StoreID);
                if (itemUpdate != null)
                {
                    itemUpdate.Phone = store.Phone;
                    itemUpdate.Location = store.Location;
                    itemUpdate.City = store.City;
                    itemUpdate.Images = store.Images;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteStore(int id)
        {
            try
            {
                var itemDelete = GetStoryByID(id);
                if (itemDelete != null)
                {
                    db.STOREs.Remove(itemDelete);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<STORE> GetAllStories()
        {
            return db.STOREs.ToList();
        }
        #endregion

        public STORE GetStoryByID(int id)
        {
            return db.STOREs.Where(t => t.StoreID == id).FirstOrDefault();
        }
        public List<STORE> GetStoriesByLocation(string local)
        {
            return db.STOREs.Where(t => t.Location == local).ToList();
        }
    }
}