using BLL.Common;
using DAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NewsBLL
    {
        NewsDAL newDal = new NewsDAL();
        public List<Model.NewsModel> GetAllNews()
        {
            EntityMapper<DAL.EF.NEWS, Model.NewsModel> mapObj = new EntityMapper<DAL.EF.NEWS, Model.NewsModel>();
            List<DAL.EF.NEWS> list = new NewsDAL().GetAllNews();
            List<Model.NewsModel> news = new List<Model.NewsModel>();
            foreach (var item in list)
            {
                news.Add(mapObj.Translate(item));
            }
            return (List<Model.NewsModel>)news;
        }
    }
}
