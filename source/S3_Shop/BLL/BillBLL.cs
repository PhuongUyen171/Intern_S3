using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAL;
using DAL.EF;
using BLL.Common;

namespace BLL
{
    public class BillBLL
    {
        private BillDAL billDal = new BillDAL();
        public List<Model.BillModel> GetAllBills()
        {
            EntityMapper<BILL, Model.BillModel> mapObj = new EntityMapper<BILL, Model.BillModel>();
            List<BILL> billList = billDal.GetAllBills();
            List<Model.BillModel> bills = new List<Model.BillModel>();
            foreach (var item in billList)
            {
                bills.Add(mapObj.Translate(item));
            }
            return bills;
        }
        public Model.BillModel GetBillByID(int id)
        {
            EntityMapper<BILL, Model.BillModel> mapObj = new EntityMapper<BILL, Model.BillModel>();
            BILL bill = billDal.GetBillByID(id);
            Model.BillModel result = mapObj.Translate(bill);
            return result;
        }
        public bool InsertBill(Model.BillModel billInsert)
        {
            EntityMapper<Model.BillModel, BILL> mapObj = new EntityMapper<Model.BillModel, BILL>();
            BILL billObj = mapObj.Translate(billInsert);
            return billDal.InsertBill(billObj);
        }
        public bool UpdateBill(Model.BillModel billUpdate)
        {
            EntityMapper<Model.BillModel, BILL> mapObj = new EntityMapper<Model.BillModel, BILL>();
            BILL billObj = mapObj.Translate(billUpdate);
            return billDal.UpdateBill(billObj);
        }
        public int? GetTotalBillByYear(DateTime date)
        {
            return billDal.GetTotalBillByYear(date);
        }
    }
}
