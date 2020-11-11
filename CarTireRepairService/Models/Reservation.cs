using System;
using System.ComponentModel.DataAnnotations;

namespace CarTireRepairService.Models
{
    public class Reservation
    {
        [Key]
        [Required]
        public Int32 ID { get; set; }

        [Required]
        public Int32 WorkshopID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReservationTime { get; set; }

        [Required]
        public ServiceTypes ProvidedService { get; set; }

        public virtual Client Client { get; set; }

        public virtual Workshop Workshop { get; set; }
    }
}
