using API.Models;
using BLL;
using log4net;
using Model;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    public class User_APIController : ApiController
    {
        private CustomerBLL cusBll = new CustomerBLL();
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string Secret = "ThisIsUyenSuperSuperSecretKey1hai3bon5sau7tam";


        [EnableCors(origins: "*", headers: "*", methods: "*")]
        
        [HttpGet]
        public HttpResponseMessage ValidLogin(string user, string pass)
        {
            if (user == "admin" && pass == "admin")
                return Request.CreateResponse(HttpStatusCode.OK, TokenManager.GenerateToken(user));
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Not valid");
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
        //[HttpGet]
        //public string Validate(string token, string username)
        //{
        //    if (account_BLL.UserNameIsExitst(username)) return "Invalid User";
        //    string tokenUsername = TokenManager.ValidateToken(token);
        //    if (username.Equals(tokenUsername))
        //    {
        //        return "Valid token";
        //    }
        //    else
        //        return "Invalid token";
        //}

        #region Get information
        [HttpGet]
        public string GetToken(string username)
        {
            try
            {
                var token= TokenManager.GenerateToken(username);
                return token;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public CustomerModel GetCustomerByToken(string token)
        {
            try
            {
                string tokenUsername = TokenManager.ValidateToken(token);
                log.Info("Get token API successful.");
                return cusBll.GetCustomerByUsername(tokenUsername);
            }
            catch (Exception ex)
            {
                log.Error("Cannot get customer by token api " + ex);
                throw;
            }
        }
        public CustomerModel GetCustomerByUsername(string user)
        {
            try
            {
                //string tokenUsername = TokenManager.ValidateToken(token);
                //if (user.Equals(tokenUsername))
                return cusBll.GetCustomerByUsername(user);
                //return null;
            }
            catch (Exception ex)
            {
                log.Error("Cannot response result." + ex);
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
        #endregion

        //[HttpPut]
        //[Route("UpdateUserWithToken/{token}/{username}/")]
        //public int UpdateUserWithToken(string token, string username, DtoUserInfo dtoUserInfo)
        //{
        //    if (!account_BLL.UserNameIsExitst(username)) return -2; //User name is not exist
        //    string tokenUsername = TokenManager.ValidateToken(token);
        //    if (username.Equals(tokenUsername))
        //    {
        //        if (user_BLL.UpdateUser(dtoUserInfo))
        //            return 1; //Update success
        //        return -1; //Failed to update
        //    }
        //    else
        //        return -3; //Invalid token
        //}
    }
}
