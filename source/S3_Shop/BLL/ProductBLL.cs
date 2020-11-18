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
    public class ProductBLL
    {
        ProductDAL proDal = new ProductDAL();
        #region Get Information 
        public List<ProductModel> GetAllProducts()
        {
            EntityMapper<PRODUCT, ProductModel> mapObj = new EntityMapper<DAL.EF.PRODUCT, Model.ProductModel>();
            List<PRODUCT> list = new ProductDAL().GetAllProducts();
            List<ProductModel> products = new List<Model.ProductModel>();
            foreach (var item in list)
            {
                products.Add(mapObj.Translate(item));
            }
            return (List<ProductModel>)products;
        }
        public ProductModel GetProductByID(int id)
        {
            EntityMapper<PRODUCT, ProductModel> mapObj = new EntityMapper<DAL.EF.PRODUCT, Model.ProductModel>();
            PRODUCT product = proDal.GetProductByID(id);
            ProductModel result = new ProductModel();
            result = mapObj.Translate(product);
            return result;
        }
        public List<ProductModel> GetProductsByCateID(int id)
        {
            EntityMapper<DAL.EF.PRODUCT, Model.ProductModel> mapObj = new EntityMapper<DAL.EF.PRODUCT, Model.ProductModel>();
            List<DAL.EF.PRODUCT> list = new ProductDAL().GetProductsByCategoryID(id);
            List<Model.ProductModel> products = new List<Model.ProductModel>();
            foreach (var item in list)
            {
                products.Add(mapObj.Translate(item));
            }
            return (List<Model.ProductModel>)products;
        }
        public List<ProductModel> GetNewProductsByCount(int count)
        {
            EntityMapper<DAL.EF.PRODUCT, Model.ProductModel> mapObj = new EntityMapper<DAL.EF.PRODUCT, Model.ProductModel>();
            List<DAL.EF.PRODUCT> list = new ProductDAL().GetNewProductsByCount(count);
            List<Model.ProductModel> products = new List<Model.ProductModel>();
            foreach (var item in list)
            {
                products.Add(mapObj.Translate(item));
            }
            return (List<Model.ProductModel>)products;
        }

        public List<ProductModel> GetProductsBySearch(string tim)
        {
            EntityMapper<DAL.EF.PRODUCT, Model.ProductModel> mapObj = new EntityMapper<DAL.EF.PRODUCT, Model.ProductModel>();
            List<DAL.EF.PRODUCT> list = new ProductDAL().GetProductsBySearch(tim);
            List<Model.ProductModel> products = new List<Model.ProductModel>();
            foreach (var item in list)
            {
                products.Add(mapObj.Translate(item));
            }
            return (List<Model.ProductModel>)products;
        }
        #endregion

        #region Insert, Update, Delete
        public bool InsertProduct(ProductModel proInsert)
        {
            EntityMapper<ProductModel, PRODUCT> mapObj = new EntityMapper<ProductModel, PRODUCT>();
            PRODUCT proObj = mapObj.Translate(proInsert);
            return proDal.InsertProduct(proObj);
        }
        public bool UpdateProduct(ProductModel proUpdate)
        {
            EntityMapper<ProductModel, PRODUCT> mapObj = new EntityMapper<ProductModel, PRODUCT>();
            PRODUCT proObj = mapObj.Translate(proUpdate);
            return proDal.UpdateProduct(proObj);
        }
        public bool DeleteProduct(int id)
        {
            return proDal.DeleteProduct(id);
        }
        #endregion
    }
}
