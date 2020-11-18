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
    public class Role_APIController : ApiController
    {
        private RoleBLL roleBll = new RoleBLL();
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public List<RoleModel> GetAllRoles()
        {
            return roleBll.GetAllRoles();
        }
        public bool InsertRole(RoleModel role)
        {
            return roleBll.InsertRole(role);
        }
        public bool DeleteRole(int id)
        {
            return roleBll.DeleteRole(id);
        }
        public bool UpdateRole(RoleModel role)
        {
            return roleBll.UpdateRole(role);
        }
        public RoleModel GetDetailRole(int id)
        {
            return roleBll.GetRoleByID(id);
        }
    }
}