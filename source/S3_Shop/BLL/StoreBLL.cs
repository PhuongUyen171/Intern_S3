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
    public class StoreBLL
    {
        private StoreDAL storeDal = new StoreDAL();
        public List<StoreModel> GetAllStores()
        {
            EntityMapper<STORE, Model.StoreModel> mapObj = new EntityMapper<STORE, Model.StoreModel>();
            List<STORE> storeList = storeDal.GetAllStories();
            List<Model.StoreModel> stores = new List<Model.StoreModel>();
            foreach (var item in storeList)
            {
                stores.Add(mapObj.Translate(item));
            }
            return stores;
        }
        public StoreModel GetStoreByID(int id)
        {
            EntityMapper<STORE, Model.StoreModel> mapObj = new EntityMapper<STORE, Model.StoreModel>();
            STORE store = storeDal.GetStoryByID(id);
            Model.StoreModel result = mapObj.Translate(store);
            return result;
        }
        public List<StoreModel> GetStoresByLocation(string local)
        {
            EntityMapper<STORE, Model.StoreModel> mapObj = new EntityMapper<STORE, Model.StoreModel>();
            List<STORE> storeList = storeDal.GetStoriesByLocation(local);
            List<Model.StoreModel> stores = new List<Model.StoreModel>();
            foreach (var item in storeList)
            {
                stores.Add(mapObj.Translate(item));
            }
            return stores;
        }
    }
}
