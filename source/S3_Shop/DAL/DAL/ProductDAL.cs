using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

namespace DAL.DAL
{
    public class ProductDAL
    {
        private S3ShopDbContext db = new S3ShopDbContext();
        public ProductDAL()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        #region CRUD
        public List<PRODUCT> GetAllProducts()
        {
            return db.PRODUCTs.ToList();
        }
        public bool InsertProduct(PRODUCT product)
        {
            try
            {
                db.PRODUCTs.Add(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateProduct(PRODUCT product)
        {
            try
            {
                var itemUpdate = GetProductByID(product.ProductID);
                if (itemUpdate != null)
                {
                    itemUpdate.ProductName = product.ProductName;
                    itemUpdate.CateID = product.CateID;
                    itemUpdate.Images = product.Images;
                    itemUpdate.Price = product.Price;
                    itemUpdate.Quantity = product.Quantity;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteProduct(int id)
        {
            try
            {
                var itemDelete = GetProductByID(id);
                if (itemDelete != null)
                {
                    db.PRODUCTs.Remove(itemDelete);
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
        public PRODUCT GetProductByID(int id)
        {
            return db.PRODUCTs.Where(t => t.ProductID == id).FirstOrDefault();
        }
        public List<PRODUCT> GetProductsByCategoryID(int id)
        {
            return db.PRODUCTs.Where(t => t.CateID == id).Take(24).ToList();
        }
        public List<PRODUCT> GetNewProductsByCount(int count)
        {
            return db.PRODUCTs.OrderByDescending(n => n.ProductID).Take(count).ToList();
        }
        public List<PRODUCT> GetProductsBySearch(string tim)
        {
            return db.PRODUCTs.Where(t => t.ProductName.Contains(tim)).ToList();
        }
    } 
}
