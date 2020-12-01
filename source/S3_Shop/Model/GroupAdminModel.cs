using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GroupAdminModel
    {
        public GroupAdminModel() { }
        [Key]
        [Display(Name = "Mã GroupAdmin")]
        [Required(ErrorMessage = "Mã GroupAdmin không được để trống.")]
        public string GroupID { get; set; }


        [Display(Name = "Tên GroupAdmin")]
        [Required(ErrorMessage = "Tên GroupAdmin không được để trống.")]
        public string GroupName { get; set; }
    }
}
