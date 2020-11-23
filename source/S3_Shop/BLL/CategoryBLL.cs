using BLL.Common;
using DAL.DAL;
using DAL.EF;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CategoryBLL
    {
        CategoryDAL cate = new CategoryDAL();
        public List<Model.CategoryModel> GetAllCategories()
        {
            EntityMapper<CATEGORY,CategoryModel> mapObj = new EntityMapper<CATEGORY, CategoryModel>();
            List<CATEGORY> cateList = new CategoryDAL().GetAllCategories();
            List<CategoryModel> categories = new List<CategoryModel>();
            foreach (var item in cateList)
            {
                categories.Add(mapObj.Translate(item));
            }
            return (List<CategoryModel>)categories;
        }
        public Model.CategoryModel GetCategoryByID(int id)
        {
            EntityMapper<CATEGORY, CategoryModel> mapObj = new EntityMapper<CATEGORY, CategoryModel>();
            CATEGORY cate = new CategoryDAL().GetCategoryByID(id);
            CategoryModel result = new CategoryModel();
            result = mapObj.Translate(cate);
            return result;
        }
        public List<CategoryModel> GetCategoryByCount(int count)
        {
            EntityMapper<CATEGORY, CategoryModel> mapObj = new EntityMapper<CATEGORY, CategoryModel>();
            List<CATEGORY> cateList = new CategoryDAL().GetCategoryByCount(count);
            List<CategoryModel> categories = new List<CategoryModel>();
            foreach (var item in cateList)
            {
                categories.Add(mapObj.Translate(item));
            }
            return (List<CategoryModel>)categories;
        }
    }
}
