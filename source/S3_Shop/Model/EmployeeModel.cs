using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EmployeeModel
    {
        public EmployeeModel() { }
        public int EmployID { get; set; }
        public string EmployName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pass { get; set; }
        public bool Statu { get; set; }
        public string GroupID { get; set; }
    }
}
