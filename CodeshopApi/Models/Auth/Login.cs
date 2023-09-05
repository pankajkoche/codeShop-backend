using System.ComponentModel.DataAnnotations;

namespace CodeshopApi.Models.Auth
{
    public class Login
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
