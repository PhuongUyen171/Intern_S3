using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class UserLogin
    {
        [Key]
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        [StringLength(maximumLength: 500, MinimumLength = 5, ErrorMessage = "Tên đăng nhập nằm trong 5-500 kí tự")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [Display(Name = "Mật khẩu")]
        public string Password { set; get; }
        public long UserID { set; get; }
        public string FullName { set; get; }
        public UserLogin() {  }

    }
}