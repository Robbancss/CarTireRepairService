using System;
using System.Collections.Generic;
using System.Linq;
using CarTireRepairService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CarTireRepairService.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            ApplicationDbContext context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.Migrate();

            if (context.Workshops.Any())
            {
                return;
            }

            var carServiceTypes = new List<CarServices>
            {
                new CarServices
                {
                    TireReplacement = true,
                    AirConCharging = true,
                    PunctureRepair = true,
                    SuspensionAdjustment = true
                },
                new CarServices
                {
                    TireReplacement = true,
                    AirConCharging = true,
                    PunctureRepair = true,
                    SuspensionAdjustment = false
                },
                new CarServices
                {
                    TireReplacement = true,
                    AirConCharging = false,
                    PunctureRepair = true,
                    SuspensionAdjustment = false
                },
                new CarServices
                {
                    TireReplacement = false,
                    AirConCharging = false,
                    PunctureRepair = true,
                    SuspensionAdjustment = false
                }
            };

            var workShops = new List<Workshop>
            {
                new Workshop
                {
                    Name = "Fix thy Car",
                    Address = "Hogwarts School of Witchcraft and Wizardry",
                    ProvidedServices = carServiceTypes[0]
                },
                new Workshop
                {
                    Name = "The Master Mechanic",
                    Address = "Willy Wonka's Factory",
                    ProvidedServices = carServiceTypes[1]
                },
                new Workshop
                {
                    Name = "Super Quick Fix",
                    Address = "The Emerald City",
                    ProvidedServices = carServiceTypes[2]
                },
                new Workshop
                {
                    Name = "Branded Repair",
                    Address = "5. Gotham City",
                    ProvidedServices = carServiceTypes[3]
                }
            };

            foreach (var workShop in workShops)
            {
                context.Workshops.Add(workShop);
            }
            context.SaveChanges();
        }
    }
}
