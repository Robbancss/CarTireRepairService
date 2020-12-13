using Microsoft.AspNetCore.Identity;
using Persistence.Data;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTRTest
{
    public static class TestDbInitializer
    {
        private static UserManager<Client> _userManager;
        private static RoleManager<IdentityRole<int>> _roleManager;

        public static void Initialize(ApplicationDbContext context)
        {

            var carServiceTypes = new List<CarServices>
            {
                new CarServices
                {
                    ID = 1,
                    TireReplacement = true,
                    AirConCharging = true,
                    PunctureRepair = true,
                    SuspensionAdjustment = true
                },
                new CarServices
                {
                    ID = 2,
                    TireReplacement = true,
                    AirConCharging = true,
                    PunctureRepair = true,
                    SuspensionAdjustment = false
                },
                new CarServices
                {
                    ID = 3,
                    TireReplacement = true,
                    AirConCharging = false,
                    PunctureRepair = true,
                    SuspensionAdjustment = false
                },
                new CarServices
                {
                    ID = 4,
                    TireReplacement = false,
                    AirConCharging = false,
                    PunctureRepair = true,
                    SuspensionAdjustment = false
                }
            };

            var reservationServices = new List<CarServices>
            {
                new CarServices
                {
                    ID = 5,
                    AirConCharging = true,
                    PunctureRepair = false,
                    SuspensionAdjustment = false,
                    TireReplacement = false
                },
                new CarServices
                {
                    ID = 6,
                    AirConCharging = false,
                    PunctureRepair = true,
                    SuspensionAdjustment = false,
                    TireReplacement = false
                },
                new CarServices
                {
                    ID = 7,
                    AirConCharging = false,
                    PunctureRepair = false,
                    SuspensionAdjustment = true,
                    TireReplacement = false
                },
                new CarServices
                {
                    ID = 8,
                    AirConCharging = false,
                    PunctureRepair = false,
                    SuspensionAdjustment = false,
                    TireReplacement = true
                },
            };

            var reservations = new List<Reservation>
            {
                new Reservation
                {
                    ID = 1,
                    ReservationTime = new DateTime(),
                    UserID = 0,
                    Client = null,
                    ProvidedService = reservationServices[0],
                    Workshop = null
                },
                new Reservation
                {
                    ID = 2,
                    ReservationTime = new DateTime(),
                    UserID = 0,
                    Client = null,
                    ProvidedService = reservationServices[1],
                    Workshop = null
                },
                new Reservation
                {
                    ID = 3,
                    ReservationTime = new DateTime(),
                    UserID = 0,
                    Client = null,
                    ProvidedService = reservationServices[2],
                    Workshop = null
                },
                new Reservation
                {
                    ID = 4,
                    ReservationTime = new DateTime(),
                    UserID = 0,
                    Client = null,
                    ProvidedService = reservationServices[3],
                    Workshop = null
                },
                new Reservation
                {
                    ID = 5,
                    ReservationTime = new DateTime(),
                    UserID = 0,
                    Client = null,
                    ProvidedService = reservationServices[2],
                    Workshop = null
                },
                new Reservation
                {
                    ID = 6,
                    ReservationTime = new DateTime(),
                    UserID = 0,
                    Client = null,
                    ProvidedService = reservationServices[1],
                    Workshop = null
                },
            };

            var workShops = new List<Workshop>
            {
                new Workshop
                {
                    ID = 1,
                    Name = "Fix thy Car",
                    Address = "Hogwarts School of Witchcraft and Wizardry",
                    ProvidedServices = carServiceTypes[0],
                    Reservations = new List<Reservation>
                    {
                        new Reservation
                        {
                            ID = 1,
                            ReservationTime = new DateTime(),
                            UserID = 0,
                            Client = new Client(),
                            ProvidedService = reservationServices[0],
                        },
                        new Reservation
                        {
                            ID = 2,
                            ReservationTime = new DateTime(),
                            UserID = 0,
                            Client = new Client(),
                            ProvidedService = reservationServices[1],
                        }, 
                    }
                },
                new Workshop
                {
                    ID = 2,
                    Name = "The Master Mechanic",
                    Address = "Willy Wonka's Factory",
                    ProvidedServices = carServiceTypes[1],
                    Reservations = new List<Reservation>
                    {
                        new Reservation
                        {
                            ID = 3,
                            ReservationTime = new DateTime(),
                            UserID = 0,
                            Client = new Client(),
                            ProvidedService = reservationServices[3],
                        },
                        new Reservation
                        {
                            ID = 4,
                            ReservationTime = new DateTime(),
                            UserID = 0,
                            Client = new Client(),
                            ProvidedService = reservationServices[2],
                        },
                    }
                },
                new Workshop
                {
                    ID = 3,
                    Name = "Super Quick Fix",
                    Address = "The Emerald City",
                    ProvidedServices = carServiceTypes[2],
                    Reservations = new List<Reservation>
                    {
                        new Reservation
                        {
                            ID = 5,
                            ReservationTime = new DateTime(),
                            UserID = 0,
                            Client = new Client(),
                            ProvidedService = reservationServices[0],
                        },
                        new Reservation
                        {
                            ID = 6,
                            ReservationTime = new DateTime(),
                            UserID = 0,
                            Client = new Client(),
                            ProvidedService = reservationServices[1],
                        },
                    }
                },
                new Workshop
                {
                    ID = 4,
                    Name = "Branded Repair",
                    Address = "5. Gotham City",
                    ProvidedServices = carServiceTypes[3],
                    Reservations = new List<Reservation>
                    {
                        new Reservation
                        {
                            ID = 7,
                            ReservationTime = new DateTime(),
                            UserID = 0,
                            Client = new Client(),
                            ProvidedService = reservationServices[0],
                            Workshop = null
                        },
                        new Reservation
                        {
                            ID = 8,
                            ReservationTime = new DateTime(),
                            UserID = 0,
                            Client = new Client(),
                            ProvidedService = reservationServices[1],
                            Workshop = null
                        },
                    }
                }
            };

            //reservations[0].Workshop = new List<Workshop>(workShops)[0];
            //reservations[1].Workshop = new List<Workshop>(workShops)[1];
            //reservations[2].Workshop = new List<Workshop>(workShops)[2];
            //reservations[3].Workshop = new List<Workshop>(workShops)[3];
            //reservations[4].Workshop = new List<Workshop>(workShops)[1];
            //reservations[5].Workshop = new List<Workshop>(workShops)[2];

            //AddAdmin();

            foreach (var workShop in workShops)
            {
                context.Workshops.Add(workShop);
            }

            //foreach (var res in reservations)
            //{
            //    context.Reservations.Add(res);
            //}
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
