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
    public class BillInfoBLL
    {
        private BillInfoDAL infoDal = new BillInfoDAL();
        public List<Model.BillInfoModel> GetBillInfoByID(int id)
        {
            EntityMapper<BILLINFO, Model.BillInfoModel> mapObj = new EntityMapper<BILLINFO, Model.BillInfoModel>();
            List<BILLINFO> infoList = infoDal.GetAllBillInfoByID(id);
            List<Model.BillInfoModel> infors = new List<Model.BillInfoModel>();
            foreach (var item in infoList)
            {
                infors.Add(mapObj.Translate(item));
            }
            return infors;
        }
        public bool InsertBillInfor(Model.BillInfoModel infoInsert)
        {
            EntityMapper<Model.BillInfoModel, BILLINFO> mapObj = new EntityMapper<Model.BillInfoModel, BILLINFO>();
            BILLINFO infoObj = mapObj.Translate(infoInsert);
            return infoDal.InsertBillInfo(infoObj);
        }
        public bool UpdateBillInfor(Model.BillInfoModel infoUpdate)
        {
            EntityMapper<Model.BillInfoModel, BILLINFO> mapObj = new EntityMapper<Model.BillInfoModel, BILLINFO>();
            BILLINFO infoObj = mapObj.Translate(infoUpdate);
            return infoDal.UpdateBillInfo(infoObj);
        }
        public bool DeleteBillInfor(int id,int productId)
        {
            return infoDal.DeleteBillInfo(id,productId);
        }
    }
}
