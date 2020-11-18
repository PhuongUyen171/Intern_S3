using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Model;

namespace API.Areas.Admin.Controllers
{
    public class Employee_APIController : ApiController
    {
        private EmployeeBLL empBLL = new EmployeeBLL();
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public List<EmployeeModel> GetAllEmployees()
        {
            return empBLL.GetAllEmployees();
        }
        public int GetLoginResultByUsernamePassword(string user, string pass)
        {
            return empBLL.LoginEmployee(user, pass);
        }
        public EmployeeModel GetEmployeeByUsername(string user)
        {
            return empBLL.GetEmployeeByUsername(user);
        }
        //public EmployeeModel GetEmployeeInforByUsernamePassword(string user, string pass)
        //{
        //    return empBLL.GetEmployeeInforByUsernamePassword(user, pass);
        //}
       
        public EmployeeModel GetEmployeeByID(int id)
        {
            return empBLL.GetEmployeeByID(id);
        }
        public List<int> GetPermisionByUsername(string name)
        {
            return empBLL.GetPermisionByUsername(name);
        }
        [HttpPut]
        public bool UpdateEmployee(EmployeeModel emUpdate)
        {
            return empBLL.UpdateEmployee(emUpdate);
        }
    }
}
