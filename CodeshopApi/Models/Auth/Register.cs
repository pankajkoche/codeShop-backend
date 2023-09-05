using System.ComponentModel.DataAnnotations;

namespace CodeshopApi.Models.Auth
{
    public class Register
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        
    }
}
