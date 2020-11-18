using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MembershipModel
    {
        public MembershipModel() { }
        public string MemID { get; set; }
        public string MemName { get; set; }
        public Nullable<int> MinPrice { get; set; }
        public Nullable<int> MaxPrice { get; set; }
        public Nullable<int> Sale { get; set; }
    }
}
