using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarTireRepairService.Models
{
    public class Client : IdentityUser
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

        public ICollection<Reservation> Reservations { get; set; }
    }
}
