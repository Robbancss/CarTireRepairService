using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarTireRepairService.Models
{
    public class Workshop
    {
        [Key]
        [Required]
        public Int32 ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public virtual CarServices ProvidedServices { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
