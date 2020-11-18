using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    public class News_APIController : ApiController
    {
        // GET: News
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public List<Model.NewsModel> GetAllNews()
        {
            return new NewsBLL().GetAllNews();
        }
    }
}