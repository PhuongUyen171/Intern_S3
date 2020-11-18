using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BillInfoModel
    {
        public BillInfoModel() { }
        public int BillID { get; set; }
        public int ProductID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> Price { get; set; }
    }
}
