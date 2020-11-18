using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Common
{
    public class EntityMapper<TSource, TDestination> where TSource : class where TDestination : class
    {
        public EntityMapper()
        {
            //1.Cateogory
            Mapper.CreateMap<Model.CategoryModel,DAL.EF.CATEGORY>();
            Mapper.CreateMap<DAL.EF.CATEGORY, Model.CategoryModel>();
            //2.News
            Mapper.CreateMap<Model.NewsModel, DAL.EF.NEWS>();
            Mapper.CreateMap<DAL.EF.NEWS, Model.NewsModel>();
            //3.Product
            Mapper.CreateMap<Model.ProductModel, DAL.EF.PRODUCT>();
            Mapper.CreateMap<DAL.EF.PRODUCT, Model.ProductModel>();
            //4.Role
            Mapper.CreateMap<Model.RoleModel, DAL.EF.ROLE>();
            Mapper.CreateMap<DAL.EF.ROLE, Model.RoleModel>();
            //5.Customer
            Mapper.CreateMap<Model.CustomerModel, DAL.EF.CUSTOMER>();
            Mapper.CreateMap<DAL.EF.CUSTOMER, Model.CustomerModel>();
            //6.Employee
            Mapper.CreateMap<Model.EmployeeModel, DAL.EF.EMPLOYEE>();
            Mapper.CreateMap<DAL.EF.EMPLOYEE, Model.EmployeeModel>();
            //7.Membership
            Mapper.CreateMap<Model.MembershipModel, DAL.EF.MEMBERSHIP>();
            Mapper.CreateMap<DAL.EF.MEMBERSHIP, Model.MembershipModel>();
            //8.Voucher
            Mapper.CreateMap<Model.VoucherModel, DAL.EF.VOUCHER>();
            Mapper.CreateMap<DAL.EF.VOUCHER, Model.VoucherModel>();
            //9.Store
            Mapper.CreateMap<Model.StoreModel, DAL.EF.STORE>();
            Mapper.CreateMap<DAL.EF.STORE, Model.StoreModel>();
            //10.Bill
            Mapper.CreateMap<Model.BillModel, DAL.EF.BILL>();
            Mapper.CreateMap<DAL.EF.BILL, Model.BillModel>();
            //11.Bill info
            Mapper.CreateMap<Model.BillInfoModel, DAL.EF.BILLINFO>();
            Mapper.CreateMap<DAL.EF.BILLINFO, Model.BillInfoModel>();
            //12.Permission
            Mapper.CreateMap<Model.PermisionModel, DAL.EF.PERMISION>();
            Mapper.CreateMap<DAL.EF.PERMISION, Model.PermisionModel>();
            //13.Group admin
            Mapper.CreateMap<Model.GroupAdminModel, DAL.EF.GROUPADMIN>();
            Mapper.CreateMap<DAL.EF.GROUPADMIN, Model.GroupAdminModel>();

        }
        public TDestination Translate(TSource obj)
        {
            return Mapper.Map<TDestination>(obj);
        }
    }
}