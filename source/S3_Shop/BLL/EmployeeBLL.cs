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
    public class EmployeeBLL
    {
        private EmployeeDAL employDal = new EmployeeDAL();
        public List<Model.EmployeeModel> GetAllEmployees()
        {
            EntityMapper<EMPLOYEE, Model.EmployeeModel> mapObj = new EntityMapper<EMPLOYEE, Model.EmployeeModel>();
            List<EMPLOYEE> employList = new EmployeeDAL().GetAllEmployees();
            List<Model.EmployeeModel> employees = new List<Model.EmployeeModel>();
            foreach (var item in employList)
            {
                employees.Add(mapObj.Translate(item));
            }
            return employees;
        }
        public List<int> GetPermisionByUsername(string name)
        {
            List<int> result = employDal.GetListPermisionByUsername(name);
            return result;
        }
        public bool CheckEmployeeExist(string user, string pass)
        {
            return employDal.CheckEmployeeExist(user, pass);
        }
        public bool InsertEmployee(Model.EmployeeModel employInsert)
        {
            EntityMapper<Model.EmployeeModel, EMPLOYEE> mapObj = new EntityMapper<Model.EmployeeModel, EMPLOYEE>();
            EMPLOYEE employObj = mapObj.Translate(employInsert);
            return employDal.InsertEmployee(employObj);
        }
        public bool UpdateEmployee(EmployeeModel employUpdate)
        {
            EntityMapper<EmployeeModel, EMPLOYEE> mapObj = new EntityMapper<EmployeeModel, EMPLOYEE>();
            EMPLOYEE employObj = mapObj.Translate(employUpdate);
            return employDal.UpdateEmployee(employObj);
        }
        public bool DeleteEmployee(int id)
        {
            return employDal.DeleteEmployee(id);
        }
        public EmployeeModel GetEmployeeByID(int id)
        {
            EntityMapper<EMPLOYEE, EmployeeModel> mapObj = new EntityMapper<EMPLOYEE, EmployeeModel>();
            EMPLOYEE employ = employDal.GetEmployeeByID(id);
            EmployeeModel result = mapObj.Translate(employ);
            return result;
        }
        public EmployeeModel GetEmployeeByUsername(string user)
        {
            EntityMapper<EMPLOYEE, EmployeeModel> mapObj = new EntityMapper<EMPLOYEE, EmployeeModel>();
            EMPLOYEE employ = employDal.GetEmployeeByUsername(user);
            EmployeeModel result = mapObj.Translate(employ);
            return result;
        }
        public int LoginEmployee(string user, string pass)
        {
            //0: tài khoản ko tồn tại
            //-1: Tài khoản đang bị khóa
            //-2: Mật khẩu không đúng
            //1: Thành công
            return employDal.GetLoginResultByUsernamePassword(user, pass);
        }
        //public EmployeeModel GetEmployeeInforByUsernamePassword(string user, string pass)
        //{
        //    EntityMapper<EMPLOYEE, EmployeeModel> mapObj = new EntityMapper<EMPLOYEE, EmployeeModel>();
        //    EMPLOYEE employ = employDal.GetEmployeeInforByUsernamePassword(user, pass);
        //    EmployeeModel result = mapObj.Translate(employ);
        //    return result;
        //}
        //public bool UpdateStatusEmployee(EmployeeModel employUpdate)
        //{
        //    EntityMapper<Model.EmployeeModel, EMPLOYEE> mapObj = new EntityMapper<Model.EmployeeModel, EMPLOYEE>();
        //    EMPLOYEE employObj = mapObj.Translate(employUpdate);
        //    return employDal.ChangeStatus(employUpdate.EmployID);
        //}
    }
}
