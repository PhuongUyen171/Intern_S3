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
    public class Membership_APIController : ApiController
    {
        private MembershipBLL memBll = new MembershipBLL();
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public List<MembershipModel> GetAllMemberships()
        {
            return memBll.GetAllMemberships();
        }
        public MembershipModel GetDetailMembership(string id)
        {
            return memBll.GetMembershipByID(id);
        }
        public bool InsertMembership(MembershipModel memInsert)
        {
            return memBll.InsertMembership(memInsert);
        }
        public bool UpdateMembership(MembershipModel memUpdate)
        {
            return memBll.UpdateMembership(memUpdate);
        }
        public bool DeleteMembership(string id)
        {
            return memBll.DeleteMembership(id);
        }
    }
}