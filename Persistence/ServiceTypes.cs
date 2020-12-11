using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Models
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
    }
}
