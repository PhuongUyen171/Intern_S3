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
    public class GroupAdmin_APIController : ApiController
    {
        GroupAdminBLL grBll = new GroupAdminBLL();
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public List<GroupAdminModel> GetAllGroupAdmins()
        {
            return grBll.GetAllGroupAdmin();
        }
        public bool InsertGroupAdmin(GroupAdminModel grInsert)
        {
            return grBll.InsertGroupAdmin(grInsert);
        }
        public bool DeleteGroupAdmin(string id)
        {
            return grBll.DeleteGroupAdmin(id);
        }
        public bool UpdateGroupAdmin(GroupAdminModel grUpdate)
        {
            return grBll.UpdateGroupAdmin(grUpdate);
        }
        public GroupAdminModel GetDetailGroupAdmin(string id)
        {
            return grBll.GetGroupAdminByID(id);
        }
    }
}
