using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

namespace DAL.DAL
{
    public  class CategoryDAL
    {
         S3ShopDbContext db=new S3ShopDbContext();
        public CategoryDAL()
        {
            db = new S3ShopDbContext();
            db.Configuration.ProxyCreationEnabled = false;
        }

        #region CRUD
        public bool InsertCategory(CATEGORY c)
        {
            try
            {
                db.CATEGORies.Add(c);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteCategory(int id)
        {
            try
            {
                var itemDelete = GetCategoryByID(id);
                if (itemDelete != null)
                {
                    db.CATEGORies.Remove(itemDelete);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateCategory(CATEGORY c)
        {
            try
            {
                var itemUpdate = GetCategoryByID(c.CateID);
                if (itemUpdate != null)
                {
                    itemUpdate.CateName = c.CateName;
                    itemUpdate.Images = c.Images;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<CATEGORY> GetAllCategories()
        {
            return db.CATEGORies.Select(t=>t).ToList();
        }
        #endregion

        public CATEGORY GetCategoryByID(int id)
        {
            return db.CATEGORies.Where(t => t.CateID == id).FirstOrDefault();
        }
    }
}
