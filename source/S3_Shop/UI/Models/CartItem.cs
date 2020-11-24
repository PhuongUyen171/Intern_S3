using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    [Serializable]
    public class CartItem
    {
        //public int Quantity;
        //public ProductModel product;
        public ProductModel Product { set; get; }
        public int Quantity { set; get; }
        public CartItem(ProductModel model,int quantity)
        {
            this.Product = model;
            if (quantity == null || quantity < 1)
                Quantity = 1;
            else Quantity = quantity;

        }
    }
}