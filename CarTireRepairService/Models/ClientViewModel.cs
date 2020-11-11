using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarTireRepairService.Models
{
    public class LoginViewModel
    {
        [DisplayName("Email")]
        [Required]
        public string Email { get; set; }

        [DisplayName("Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        [DisplayName("Name")]
        [Required]
        public string FullName { get; set; }

        [DisplayName("Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Car Name")]
        [Required]
        public string CarName { get; set; }

        [DisplayName("License Number")]
        [Required]
        public string LicenseNumber { get; set; }

    }
}
