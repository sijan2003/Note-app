using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.viewmodel
{
    public class Login
    {
        [Required]
        public string? Username { get; set; }
        [Required] 
        public string? Password { get; set; }

        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
    }
}
