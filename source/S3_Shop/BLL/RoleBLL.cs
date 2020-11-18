using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BLL.Common;
using DAL.DAL;
using DAL.EF;
using Model;

namespace BLL
{
    public class RoleBLL
    {
        RoleDAL roleDal = new RoleDAL();
        public List<Model.RoleModel> GetAllRoles()
        {
            EntityMapper<DAL.EF.ROLE, Model.RoleModel> mapObj = new EntityMapper<DAL.EF.ROLE, Model.RoleModel>();
            List<DAL.EF.ROLE> list = new RoleDAL().GetAllRoles();
            List<Model.RoleModel> roles = new List<Model.RoleModel>();
            foreach (var item in list)
            {
                roles.Add(mapObj.Translate(item));
            }
            return (List<Model.RoleModel>)roles;
        }
        public bool InsertRole(Model.RoleModel role)
        {
            EntityMapper<Model.RoleModel, ROLE> mapObj = new EntityMapper<Model.RoleModel, ROLE>();
            ROLE roleObj = new ROLE();
            roleObj = mapObj.Translate(role);
            return roleDal.InsertRole(roleObj);
        }
        public bool UpdateRole(Model.RoleModel role)
        {
            EntityMapper<Model.RoleModel, ROLE> mapObj = new EntityMapper<Model.RoleModel, ROLE>();
            ROLE roleObj = new ROLE();
            roleObj = mapObj.Translate(role);
            return roleDal.UpdateRole(roleObj);
        }
        public bool DeleteRole(int id)
        {
            return roleDal.DeleteRole(id);
        }
        public RoleModel GetRoleByID(int id)
        {
            EntityMapper<DAL.EF.ROLE, Model.RoleModel> mapObj = new EntityMapper<DAL.EF.ROLE, Model.RoleModel>();
            ROLE role = new RoleDAL().GetRoleByID(id);
            RoleModel result = new RoleModel();
            result = mapObj.Translate(role);
            return result;
        }
    }
}
