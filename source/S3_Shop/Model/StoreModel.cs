using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StoreModel
    {
        public StoreModel() { }
        public int StoreID { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Images { get; set; }
    }
}
