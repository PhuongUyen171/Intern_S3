using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class VoucherModel
    {
        public VoucherModel() { }
        public string VoucherID { get; set; }
        public string Title { get; set; }
        public Nullable<int> Sale { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Images { get; set; }
    }
}
