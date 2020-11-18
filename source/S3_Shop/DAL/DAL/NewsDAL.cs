using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

namespace DAL.DAL
{
    public class NewsDAL
    {
        private S3ShopDbContext db = new S3ShopDbContext();
        public NewsDAL()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        #region CRUD
        public List<NEWS> GetAllNews() 
        {
            return db.NEWS.ToList();
        }
        public bool InsertNews(NEWS news)
        {
            try
            {
                db.NEWS.Add(news);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateNews(NEWS news)
        {
            try
            {
                var itemUpdate = GetNewsByID(news.NewsID);
                if (itemUpdate != null)
                {
                    itemUpdate.Descrip = news.Descrip;
                    itemUpdate.Images = news.Images;
                    itemUpdate.PublishDate = news.PublishDate;
                    itemUpdate.Title = news.Title;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteNews(int id)
        {
            try
            {
                var itemDelete = GetNewsByID(id);
                if (itemDelete != null)
                {
                    db.NEWS.Remove(itemDelete);
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
        public NEWS GetNewsByID(int id)
        {
            return db.NEWS.FirstOrDefault(t=>t.NewsID==id);
        }
    }
}
