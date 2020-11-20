using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using System.Web.Routing;
using UI.Models;

namespace Model.Common
{
    class HasPermisionAttribute:AuthorizeAttribute
    {
        public int RoleID { set; get; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = (LoginModel)HttpContext.Current.Session[Constants.ADMIN_SESSION];
            if (session == null)
            {
                return false;
            }
            List<int> privilegeLevels = this.GetCredentialByLoggedInUser(session.UserName); // Call another method to get rights of the user from DB
            if (privilegeLevels.Contains(this.RoleID) || session.GroupID == null)
            {
                return false;
            }
            else if (privilegeLevels.Contains(this.RoleID) || session.GroupID == Constants.ADMIN_GROUP)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Areas/Admin/Views/Shared/401.cshtml"
            };
        }
        private List<int> GetCredentialByLoggedInUser(string userName)
        {
            var credentials = (List<int>)HttpContext.Current.Session[Constants.SESSION_CREDENTIALS];
            return credentials;
        }
    }
}
