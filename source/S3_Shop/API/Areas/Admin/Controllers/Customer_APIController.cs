using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using System.Web.Http.Cors;

namespace API.Areas.Admin.Controllers
{
    public class Customer_APIController : ApiController
    {
        private CustomerBLL cusBll = new CustomerBLL();
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public List<CustomerModel> GetAllCustomers()
        {
            return cusBll.GetAllCustomers();
        }
        public CustomerModel GetDetailCustomer(int id)
        {
            return cusBll.GetCustomerByID(id);
        }
        public bool InsertCustomer(CustomerModel cusInsert)
        {
            return cusBll.InsertCustomer(cusInsert);
        }
        public bool UpdateCustomer(CustomerModel cusUpdate)
        {
            return cusBll.UpdateCustomer(cusUpdate);
        }
        public bool DeleteCustomer(int id)
        {
            return cusBll.DeleteCustomer(id);
        }
        public int CountCustomer()
        {
            return cusBll.GetAllCustomers().Count();
        }
    }
}
