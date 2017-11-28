using System.ComponentModel.DataAnnotations;

namespace SteponTech.Data.BaseModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "请输入用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}