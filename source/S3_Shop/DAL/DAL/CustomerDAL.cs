using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Common;

namespace DAL.DAL
{
    public class CustomerDAL
    {
        private S3ShopDbContext db = new S3ShopDbContext();
        public CustomerDAL()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        #region CRUD
        public List<CUSTOMER> GetAllCustomers()
        {
            return db.CUSTOMERs.ToList();
        }
        public bool InsertCustomer(CUSTOMER custom)
        {
            try
            {
                custom.Pass = Encryptor.MD5Hash(custom.Pass);
                db.CUSTOMERs.Add(custom);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteCustomer(int id)
        {
            try
            {
                var itemDelete = GetCustomerByID(id);
                if (itemDelete != null)
                {
                    db.CUSTOMERs.Remove(itemDelete);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateCustomer(CUSTOMER custom)
        {
            try
            {
                var itemUpdate = GetCustomerByID(custom.CustomID);
                if (itemUpdate != null)
                {
                    itemUpdate.CustomName = custom.CustomName;
                    itemUpdate.Email = custom.Email;
                    itemUpdate.Location = custom.Location;
                    itemUpdate.Phone = custom.Phone;
                    itemUpdate.Pass = Encryptor.MD5Hash(custom.Pass);
                    itemUpdate.MemID = custom.MemID;
                    itemUpdate.Statu = custom.Statu;
                    itemUpdate.TotalPrice = custom.TotalPrice;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        public CUSTOMER GetCustomerByID(int id)
        {
            return db.CUSTOMERs.Where(t => t.CustomID == id).FirstOrDefault();
        }
        public bool ChangeStatusCustomer(int userID)
        {
            try
            {
                var acc = db.CUSTOMERs.SingleOrDefault(x => x.CustomID==userID);
                acc.Statu = !acc.Statu;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public int GetLoginResultByUsernamePassword(string user, string pass)
        {
            //0: Tên đăng nhập hoặc mật khẩu không tồn tại
            //1: Thành công
            var cus = db.CUSTOMERs.FirstOrDefault(x => x.Username == user);
            if (cus == null)
                return 0;
            else if (cus.Pass != Encryptor.MD5Hash(pass))
                return 0;
            else
                return 1;
        }
        public CUSTOMER GetCustomerByUsername(string user)
        {
            return db.CUSTOMERs.SingleOrDefault(t => t.Username == user);
        }
        public CUSTOMER GetCustomerByEmail(string mail)
        {
            return db.CUSTOMERs.FirstOrDefault(t => t.Email == mail);
        }
    }
}
