using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarTireRepairService.Models
{
    public class Reservation
    {
        [Key]
        [Required]
        public Int32 ID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReservationTime { get; set; }

        [Required]
        public ICollection<CarServices> ProvidedService { get; set; }

        public virtual Client Client { get; set; }

        public virtual Workshop Workshop { get; set; }
    }
}
