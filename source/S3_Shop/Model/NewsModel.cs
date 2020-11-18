using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class NewsModel
    {
        public NewsModel() { }
        public int NewsID { get; set; }
        public string Title { get; set; }
        public string Images { get; set; }
        public string Descrip { get; set; }
        public Nullable<System.DateTime> PublishDate { get; set; }
    }
}
