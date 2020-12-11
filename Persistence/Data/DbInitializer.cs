using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Models;

namespace Persistence.Data
{
    public class DbInitializer
    {
        private static UserManager<Client> _userManager;
        private static RoleManager<IdentityRole<int>> _roleManager;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            ApplicationDbContext context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _userManager = serviceProvider.GetRequiredService<UserManager<Client>>();
            _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            context.Database.Migrate();

            if (!context.Users.Any())
            {
                AddAdmin();
            }

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

        private static void AddAdmin()
        {
            var adminUser = new Client
            {
                UserName = "admin",
                FullName = "Adminis Trator",
                Email = "admin@example.com",
                CarName = "AAA",
                LicenseNumber = "000000"
            };
            var adminPassword = "admin";
            var adminRole = new IdentityRole<int>("administrator");

            var result1 = _userManager.CreateAsync(adminUser, adminPassword).Result;
            var result2 = _roleManager.CreateAsync(adminRole).Result;
            var result3 = _userManager.AddToRoleAsync(adminUser, adminRole.Name).Result;
        }
    }
}
