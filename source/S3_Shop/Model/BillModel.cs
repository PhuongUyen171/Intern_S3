using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BillModel
    {
        public BillModel() { }
        public int BillID { get; set; }
        public Nullable<System.DateTime> PublishDate { get; set; }
        public Nullable<int> ToTalPrice { get; set; }
        public Nullable<int> CustomID { get; set; }
        public Nullable<int> EmployID { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<int> ShippingCost { get; set; }
        public Nullable<int> Sale { get; set; }
        public string Notes { get; set; }
        public Nullable<bool> Payment { get; set; }
        public string VoucherID { get; set; }
    }
}
