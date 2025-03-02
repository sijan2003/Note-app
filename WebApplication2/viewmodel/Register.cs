using System.ComponentModel.DataAnnotations;

namespace WebApplication2.viewmodel
{
    public class Register
    {
        [Required]
        [Display (Name ="First Name")]
        public string? FirstName     { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [Required]
        [Display(Name = "Email Adress")]
        [EmailAddress]
        public string ? Email   { get; set; }
        [Required]
        public string ? Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ? ConfirmPassword { get; set; }
    }

}
