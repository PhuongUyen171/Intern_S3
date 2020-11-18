using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PermisionModel
    {
        public PermisionModel() { }
        public int RoleID { get; set; }
        public string GroupID { get; set; }
        public Nullable<bool> PerID { get; set; }
    }
}
