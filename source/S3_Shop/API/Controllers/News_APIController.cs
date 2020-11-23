using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Model;

namespace API.Controllers
{
    public class News_APIController : ApiController
    {
        // GET: News
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public List<NewsModel> GetAllNews()
        {
            return new NewsBLL().GetAllNews();
        }
        public NewsModel GetDetailNews(int id)
        {
            return new NewsBLL().GetNewsByID(id);
        }
        public List<NewsModel> GetNewsByCount(int count)
        {
            return new NewsBLL().GetNewsByCount(count);
        }
    }
}