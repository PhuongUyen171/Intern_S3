using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Common;
using DAL.DAL;
using DAL.EF;

namespace BLL
{
    public class GroupAdminBLL
    {
        private GroupAdminDAL groupDal = new GroupAdminDAL();
        public List<Model.GroupAdminModel> GetAllGroupAdmin()
        {
            EntityMapper<GROUPADMIN, Model.GroupAdminModel> mapObj = new EntityMapper<GROUPADMIN, Model.GroupAdminModel>();
            List<GROUPADMIN> groupList = groupDal.GetAllGroupAdmins();
            List<Model.GroupAdminModel> groups = new List<Model.GroupAdminModel>();
            foreach (var item in groupList)
            {
                groups.Add(mapObj.Translate(item));
            }
            return groups;
        }
        public Model.GroupAdminModel GetGroupAdminByID(string id)
        {
            EntityMapper<GROUPADMIN, Model.GroupAdminModel> mapObj = new EntityMapper<GROUPADMIN, Model.GroupAdminModel>();
            GROUPADMIN grad = groupDal.GetGroupAdminByID(id);
            Model.GroupAdminModel result = mapObj.Translate(grad);
            return result;
        }
        public bool InsertGroupAdmin(Model.GroupAdminModel grInsert)
        {
            EntityMapper<Model.GroupAdminModel, GROUPADMIN> mapObj = new EntityMapper<Model.GroupAdminModel, GROUPADMIN>();
            GROUPADMIN grObj = mapObj.Translate(grInsert);
            return groupDal.InsertGroupAdmin(grObj);
        }
        public bool UpdateGroupAdmin(Model.GroupAdminModel grUpdate)
        {
            EntityMapper<Model.GroupAdminModel, GROUPADMIN> mapObj = new EntityMapper<Model.GroupAdminModel, GROUPADMIN>();
            GROUPADMIN grObj = mapObj.Translate(grUpdate);
            return groupDal.UpdateGroupAdmin(grObj);
        }
        public bool DeleteGroupAdmin(string id)
        {
            return groupDal.DeleteGroupAdmin(id);
        }
    }
}
