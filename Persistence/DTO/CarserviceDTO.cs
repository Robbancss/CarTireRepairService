using Persistence.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Persistence.DTO
{
    public class CarserviceDTO
    {
        [Key]
        [Required]
        public Int32 ID { get; set; }

        [DisplayName("Tire replacement")]
        [Required]
        public bool TireReplacement { get; set; }

        [DisplayName("Air conditioner changing")]
        [Required]
        public bool AirConCharging { get; set; }

        [DisplayName("Puncture repair")]
        [Required]
        public bool PunctureRepair { get; set; }

        [DisplayName("Suspension adjustment")]
        [Required]
        public bool SuspensionAdjustment { get; set; }

        public static explicit operator CarserviceDTO(CarServices carServices) => new CarserviceDTO
        {
            ID = carServices.ID,
            AirConCharging = carServices.AirConCharging,
            PunctureRepair = carServices.PunctureRepair,
            SuspensionAdjustment = carServices.SuspensionAdjustment,
            TireReplacement = carServices.TireReplacement
        };

        public static explicit operator CarServices(CarserviceDTO carserviceDTO) => new CarServices
        {
            ID = carserviceDTO.ID,
            AirConCharging = carserviceDTO.AirConCharging,
            PunctureRepair = carserviceDTO.PunctureRepair,
            SuspensionAdjustment = carserviceDTO.SuspensionAdjustment,
            TireReplacement = carserviceDTO.TireReplacement
        };
    }
}
