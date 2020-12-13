using Persistence.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistence.DTO
{
    public class ReservationDTO
    {
        [Key]
        [Required]
        public Int32 ID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReservationTime { get; set; }

        [Required]
        public CarServices ProvidedService { get; set; }

        public Client Client { get; set; }

        public static explicit operator ReservationDTO(Reservation reservation) => new ReservationDTO
        {
             ID = reservation.ID,
             ProvidedService = reservation.ProvidedService,
             ReservationTime = reservation.ReservationTime,
             Client = reservation.Client
        };

        public static explicit operator Reservation(ReservationDTO reservationDTO) => new Reservation
        {
            ID = reservationDTO.ID,
            ProvidedService = reservationDTO.ProvidedService,
            ReservationTime = reservationDTO.ReservationTime,
            Client = reservationDTO.Client
        };
    }
}
