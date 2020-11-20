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
        public int quantity;
        public ProductModel product;
        public ProductModel Product { set; get; }
        public int Quantity {
            set {  }
            get 
            {
                if (quantity > 1)
                    return quantity;
                return 1;
            }
        }
        public CartItem(ProductModel model,int quantity)
        {
            this.Product = model;
            quantity = 1;
        }
    }
}