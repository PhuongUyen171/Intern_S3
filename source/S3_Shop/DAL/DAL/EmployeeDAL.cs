using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common;
using DAL.EF;

namespace DAL.DAL
{
    public class EmployeeDAL
    {
        private S3ShopDbContext db = new S3ShopDbContext();
        public EmployeeDAL()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        #region CRUD
        public bool InsertEmployee(EMPLOYEE employee)
        {
            try
            {
                employee.Pass = Encryptor.MD5Hash(employee.Pass);
                db.EMPLOYEEs.Add(employee);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteEmployee(int id)
        {
            try
            {
                var itemDelete = GetEmployeeByID(id);
                if (itemDelete != null)
                {
                    db.EMPLOYEEs.Remove(itemDelete);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateEmployee(EMPLOYEE employee)
        {
            try
            {
                var itemUpdate = GetEmployeeByID(employee.EmployID);
                if (itemUpdate != null)
                {
                    itemUpdate.EmployName = employee.EmployName;
                    itemUpdate.FirstName = employee.FirstName;
                    itemUpdate.LastName = employee.LastName;
                    itemUpdate.Pass = Encryptor.MD5Hash(employee.Pass);
                    itemUpdate.Statu = employee.Statu;
                    itemUpdate.GroupID = employee.GroupID;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<EMPLOYEE> GetAllEmployees()
        {
            return db.EMPLOYEEs.ToList();
        }
        #endregion
        public EMPLOYEE GetEmployeeByID(int id)
        {
            return db.EMPLOYEEs.FirstOrDefault(t => t.EmployID == id);
        }
        public int GetLoginResultByUsernamePassword(string user, string pass)
        {
            //0: tài khoản ko tồn tại
            //-1: Tài khoản đang bị khóa
            //-2: Mật khẩu không đúng
            //-3: Khóa tài khoản vì đăng nhập quá ba lần
            //1: Thành công
            var employ = db.EMPLOYEEs.FirstOrDefault(x => x.EmployName == user);
            if (employ == null) 
                return 0;
            else if (employ.Statu == false) 
                return -1;
            else if (employ.Pass != Encryptor.MD5Hash(pass))
            {
                if (Model.Common.Constants.COUNT_LOGIN_FAIL_ADMIN == 3)
                {
                    ChangeStatusEmployee(employ.EmployID);
                    return -3;
                }
                else
                {
                    Model.Common.Constants.COUNT_LOGIN_FAIL_ADMIN++;
                    return -2;
                }    
            } 
            else 
                return 1;
        }
        public bool CheckEmployeeExist(string adminName,string pass)
        {
            return db.EMPLOYEEs.Any(t => t.EmployName==adminName & t.Pass == Encryptor.MD5Hash(pass));
        }
        public bool ChangeStatusEmployee(int id)
        {
            try
            {
                var acc = db.EMPLOYEEs.SingleOrDefault(t => t.EmployID == id);
                acc.Statu = !acc.Statu;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public EMPLOYEE GetEmployeeByUsername(string user)
        {
            return db.EMPLOYEEs.FirstOrDefault(t => t.EmployName == user);
        }
        public List<int> GetListPermisionByUsername(string username)
        {
            var user = db.EMPLOYEEs.SingleOrDefault(x => x.EmployName == username);
            var roles = (from a in db.PERMISIONs
                         join b in db.EMPLOYEEs on a.GroupID equals b.GroupID
                         join c in db.ROLES on a.RoleID equals c.RoleID
                         where b.GroupID == user.GroupID
                         select new
                         {
                             RoleID = a.RoleID,
                             GroupID = a.GroupID
                         }).AsEnumerable().Select(x => new PERMISION()
                         {
                             RoleID = x.RoleID,
                             GroupID = x.GroupID
                         });
            return roles.Select(x => x.RoleID).ToList();
        }
    }
}
