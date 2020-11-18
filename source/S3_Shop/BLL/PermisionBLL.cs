using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL.DAL;
using BLL.Common;
using DAL.EF;
using System.Runtime.InteropServices;

namespace BLL
{
    public class PermisionBLL
    {
        private PermisionDAL perDal = new PermisionDAL();
        public List<PermisionModel> GetAllPermisions()
        {
            EntityMapper<PERMISION, PermisionModel> mapObj = new EntityMapper<PERMISION, PermisionModel>();
            List<PERMISION> list = perDal.GetAllPermisions();
            List<PermisionModel> pers = new List<PermisionModel>();
            foreach (var item in list)
            {
                pers.Add(mapObj.Translate(item));
            }
            return pers;
        }
        public bool InsertPermision(PermisionModel per)
        {
            EntityMapper<PermisionModel, PERMISION> mapObj = new EntityMapper<PermisionModel, PERMISION>();
            PERMISION perObj = new PERMISION();
            perObj = mapObj.Translate(per);
            return perDal.InsertPermision(perObj);
        }
        public bool UpdatePermision(PermisionModel per)
        {
            EntityMapper<PermisionModel, PERMISION> mapObj = new EntityMapper<PermisionModel, PERMISION>();
            PERMISION perObj = new PERMISION();
            perObj = mapObj.Translate(per);
            return perDal.UpdatePermision(perObj);
        }
        public bool DeletePermision(string groupID,int roleID)
        {
            return perDal.DeletePermision(groupID,roleID);
        }
        public PermisionModel GetPermisionByID(string groupID, int roleID)
        {
            EntityMapper<PERMISION, PermisionModel> mapObj = new EntityMapper<PERMISION, PermisionModel>();
            PERMISION per = perDal.GetPermisionByID(groupID, roleID);
            PermisionModel result = mapObj.Translate(per);
            return result;
        }
    }
}
