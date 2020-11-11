using System;
using System.ComponentModel.DataAnnotations;

namespace CarTireRepairService.Models
{
    public enum ServiceTypes
    {
        TireReplacement,
        AirConCharging,
        PunctureRepair,
        SuspensionAdjustment
    }

    public class CarServices
    {
        [Key]
        [Required]
        public Int32 ID { get; set; }

        [Required]
        public bool TireReplacement { get; set; }
        
        [Required]
        public bool AirConCharging { get; set; }

        [Required]
        public bool PunctureRepair { get; set; }
        
        [Required]
        public bool SuspensionAdjustment { get; set; }
    }
}
