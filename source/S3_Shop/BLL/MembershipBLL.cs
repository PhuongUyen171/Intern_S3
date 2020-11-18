using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Common;
using DAL.DAL;
using DAL.EF;
using Model;

namespace BLL
{
    public class MembershipBLL
    {
        MembershipDAL memDal = new MembershipDAL();
        public List<Model.MembershipModel> GetAllMemberships()
        {
            EntityMapper<MEMBERSHIP, Model.MembershipModel> mapObj = new EntityMapper<MEMBERSHIP, Model.MembershipModel>();
            List<MEMBERSHIP> list = new MembershipDAL().GetAllMembership();
            List<Model.MembershipModel> mems = new List<Model.MembershipModel>();
            foreach (var item in list)
            {
                mems.Add(mapObj.Translate(item));
            }
            return (List<Model.MembershipModel>)mems;
        }
        public bool InsertMembership(Model.MembershipModel mem)
        {
            EntityMapper<Model.MembershipModel, MEMBERSHIP> mapObj = new EntityMapper<Model.MembershipModel, MEMBERSHIP>();
            MEMBERSHIP memObj = new MEMBERSHIP();
            memObj = mapObj.Translate(mem);
            return memDal.InsertMembership(memObj);
        }
        public bool UpdateMembership(Model.MembershipModel mem)
        {
            EntityMapper<Model.MembershipModel, MEMBERSHIP> mapObj = new EntityMapper<Model.MembershipModel, MEMBERSHIP>();
            MEMBERSHIP memObj = new MEMBERSHIP();
            memObj = mapObj.Translate(mem);
            return memDal.UpdateMembership(memObj);
        }
        public bool DeleteMembership(string id)
        {
            return memDal.DeleteMembership(id);
        }
        public MembershipModel GetMembershipByID(string id)
        {
            EntityMapper<MEMBERSHIP, Model.MembershipModel> mapObj = new EntityMapper<MEMBERSHIP, MembershipModel>();
            MEMBERSHIP mem = new MembershipDAL().GetMembershipByID(id);
            MembershipModel result = mapObj.Translate(mem);
            return result; 
        }
    }
}
