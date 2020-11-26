using API.Models;
using BLL;
using log4net;
using Model;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    public class User_APIController : ApiController
    {
        private CustomerBLL cusBll = new CustomerBLL();
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public int GetLoginResultByUsernamePassword(string user, string pass)
        {
            try
            {
                log.Info("Successful to response login result.");
                return cusBll.LoginCustomer(user, pass);
            }
            catch (Exception)
            {
                log.Error("Cannot response login result.");
                throw;
            }
        }
        [HttpGet]
        public HttpResponseMessage ValidLogin(string user, string pass)
        {
            if (user == "admin" && pass == "admin")
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, TokenManager.GenerateToken(user));
            else
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest,"Not valid");
        }
        //public string GetLoginResultByUsernamePassword(string user, string pass)
        //{
        //    try
        //    {
        //        log.Info("Successful to response login result.");
        //        if(cusBll.LoginCustomer(user,pass)==1)
        //            return TokenManager.GenerateToken(user);
        //        return "0";
        //    }
        //    catch (Exception)
        //    {
        //        log.Error("Cannot response login result.");
        //        throw;
        //    }
        //}
        public CustomerModel GetCustomerByUsername(string user)
        {
            try
            {
                return cusBll.GetCustomerByUsername(user);
            }
            catch (Exception)
            {
                log.Error("Cannot response result.");
                throw;
            }
        }
        public CustomerModel GetCustomerByEmail(string mail)
        {
            try
            {
                return cusBll.GetCustomerByEmail(mail);
            }
            catch (Exception)
            {
                log.Error("Cannot response result.");
                throw;
            }
        }
        [HttpPut]
        public bool UpdateCustomer(CustomerModel model)
        {
            try
            {
                return cusBll.UpdateCustomer(model);
            }
            catch (Exception)
            {
                log.Error("Cannot response result");
                throw;
            }
        }
        [HttpPost]
        public long InsertForFacebook(CustomerModel model)
        {
            try 
            { 
                return cusBll.InsertForFacebook(model);
            }
            catch (Exception)
            {
                log.Error("Cannot response result");
                throw;
            }
        }
        public long InsertForGoogle(CustomerModel model)
        {
            try
            { 
                return cusBll.InsertForGoogle(model);
            }
            catch (Exception)
            {
                log.Error("Cannot response result");
                throw;
            }
        }
        public bool InsertCustomer(CustomerModel cusInsert)
        {
            try
            { 
                return cusBll.InsertCustomer(cusInsert);
            }
            catch (Exception)
            {
                log.Error("Cannot response result");
                throw;
            }
        }
        //[Route("UserLogin")]
        //[HttpPost]
        //public ResponseVM UserLogin(userLogin objVM)
        //{
        //    var objlst = wmsEN.Usp_Login(objVM.UserName, UtilityVM.Encryptdata(objVM.Passward), "").ToList<Usp_Login_Result>().FirstOrDefault();
        //    if (objlst.Status == -1) return new ResponseVM
        //    {
        //        Status = "Invalid",
        //        Message = "Invalid User."
        //    };
        //    if (objlst.Status == 0) return new ResponseVM
        //    {
        //        Status = "Inactive",
        //        Message = "User Inactive."
        //    };
        //    else return new ResponseVM
        //    {
        //        Status = "Success",
        //        Message = TokenManager.GenerateToken(objVM.UserName)
        //    };
        //}
    }
}
