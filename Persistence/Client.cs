using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Models
{
    public class Client : IdentityUser<int>
    {
        [Required]
        [MaxLength(30)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(15)]
        public string CarName { get; set; }

        [Required]
        [MaxLength(6)]
        [MinLength(6)]
        public string LicenseNumber { get; set; }
    }
}
