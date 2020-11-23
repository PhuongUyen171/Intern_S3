using BLL.Common;
using DAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL.EF;

namespace BLL
{
    public class NewsBLL
    {
        NewsDAL newDal = new NewsDAL();
        public List<NewsModel> GetAllNews()
        {
            EntityMapper<NEWS, NewsModel> mapObj = new EntityMapper<NEWS, NewsModel>();
            List<NEWS> list = new NewsDAL().GetAllNews();
            List<NewsModel> news = new List<NewsModel>();
            foreach (var item in list)
                news.Add(mapObj.Translate(item));
            return (List<NewsModel>)news;
        }
        public NewsModel GetNewsByID(int id)
        {
            EntityMapper<NEWS, NewsModel> mapObj = new EntityMapper<NEWS, NewsModel>();
            NEWS news = new NewsDAL().GetNewsByID(id);
            NewsModel result = mapObj.Translate(news);
            return result;
        }
        public List<NewsModel> GetNewsByCount(int count)
        {
            EntityMapper<NEWS, NewsModel> mapObj = new EntityMapper<NEWS, NewsModel>();
            List<NEWS> list = new NewsDAL().GetNewsByCount(count);
            List<NewsModel> news = new List<NewsModel>();
            foreach (var item in list)
                news.Add(mapObj.Translate(item));
            return (List<NewsModel>)news;
        }
    }
}
