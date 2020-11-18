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
    public class VoucherBLL
    {
        private VoucherDAL vouDal = new VoucherDAL();
        public List<VoucherModel> GetAllVouchers()
        {
            EntityMapper<VOUCHER, VoucherModel> mapObj = new EntityMapper<VOUCHER,VoucherModel>();
            List<VOUCHER> voucherList = vouDal.GetAllVouchers();
            List<VoucherModel> vouchers = new List<VoucherModel>();
            foreach (var item in voucherList)
            {
                vouchers.Add(mapObj.Translate(item));
            }
            return vouchers;
        }
        public Model.VoucherModel GetVoucherByID(string id)
        {
            EntityMapper<VOUCHER, Model.VoucherModel> mapObj = new EntityMapper<VOUCHER, Model.VoucherModel>();
            VOUCHER vou = vouDal.GetVoucherByID(id);
            Model.VoucherModel result = mapObj.Translate(vou);
            return result;
        }
        public bool InsertVoucher(Model.VoucherModel vouInsert)
        {
            EntityMapper<Model.VoucherModel, VOUCHER> mapObj = new EntityMapper<Model.VoucherModel, VOUCHER>();
            VOUCHER vouObj = mapObj.Translate(vouInsert);
            return vouDal.InsertVoucher(vouObj);
        }
        public bool UpdateVoucher(Model.VoucherModel vouUpdate)
        {
            EntityMapper<Model.VoucherModel, VOUCHER> mapObj = new EntityMapper<Model.VoucherModel, VOUCHER>();
            VOUCHER vouObj = mapObj.Translate(vouUpdate);
            return vouDal.UpdateVoucher(vouObj);
        }
        public bool DeleteVoucher(string id)
        {
            return vouDal.DeleteVoucher(id);
        }
    }
}
