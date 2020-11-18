using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using Antlr.Runtime.Tree;
using BLL;
using Model;

namespace API.Controllers
{
    public class User_APIController : ApiController
    {
        private CustomerBLL cusBll = new CustomerBLL();
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public int GetLoginResultByUsernamePassword(string user, string pass)
        {
            return cusBll.LoginCustomer(user, pass);
        }
        public CustomerModel GetCustomerByUsername(string user)
        {
            return cusBll.GetCustomerByUsername(user);
        }
        public CustomerModel GetCustomerByEmail(string mail)
        {
            return cusBll.GetCustomerByEmail(mail);
        }
        [HttpPut]
        public bool UpdateCustomer(CustomerModel model)
        {
            return cusBll.UpdateCustomer(model);
        }
    }
}
