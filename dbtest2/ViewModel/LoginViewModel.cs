using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dbtest2.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "사용자 이메일을 입력하세요.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "사용자 비밀번호를 입력하세요.")]
        public string Password { get; set; }
    }
}