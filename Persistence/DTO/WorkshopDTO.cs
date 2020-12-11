using Persistence.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Persistence.DTO
{
    public class WorkshopDTO
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

        public static explicit operator Workshop(WorkshopDTO workshopDTO) => new Workshop
        {
            ID = workshopDTO.ID,
            Address = workshopDTO.Address,
            Name = workshopDTO.Name,
            ProvidedServices = workshopDTO.ProvidedServices
        };

        public static explicit operator WorkshopDTO(Workshop workshop) => new WorkshopDTO
        {
            ID = workshop.ID,
            Address = workshop.Address,
            Name = workshop.Name,
            ProvidedServices = workshop.ProvidedServices
        };
    }
}
