using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ProductModel
    {
        public ProductModel() { }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Images { get; set; }
        public Nullable<bool> Statu { get; set; }
        public Nullable<int> CategoryID { get; set; }

    }
}
