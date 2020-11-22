using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public CarServices ProvidedService { get; set; }

        [ForeignKey("Client")]
        [Required]
        public String UserID { get; set; }
        public Client Client { get; set; }

        public virtual Workshop Workshop { get; set; }
    }
}
