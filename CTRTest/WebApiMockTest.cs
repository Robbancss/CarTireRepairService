using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.DTO;
using Persistence.Models;
using Persistence.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WebAPI.Controller;
using Xunit;

namespace CTRTest
{
    public class WebApiMockTest : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly CTRService _service;
        private readonly ReservationController _reservationController;
        private readonly WorkshopsController _workshopsController;
        private readonly AccountController _accountController;

        private static UserManager<Client> _userManager;
        private static RoleManager<IdentityRole<int>> _roleManager;
        private static SignInManager<Client> _signInManager;

        public WebApiMockTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new ApplicationDbContext(options);
            TestDbInitializer.Initialize(_context);
            _service = new CTRService(_context);
            //_accountController = new AccountController(_signInManager);
            _workshopsController = new WorkshopsController(_service);
            _reservationController = new ReservationController(_service);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void GetWorkshopsTest()
        {
            var result = _workshopsController.GetWorkshops();

            var content = Assert.IsAssignableFrom<IEnumerable<WorkshopDTO>>(result.Value);
            Assert.Equal(4, content.Count());
        }

        [Fact]
        public void GetWorkshopReservationByID1Test()
        {
            var result = _workshopsController.GetWorkshopReservationsByID(1);

            var content = Assert.IsAssignableFrom<IEnumerable<ReservationDTO>>(result.Value);
            Assert.Equal(2, content.Count());
        }

        [Fact]
        public void GetWorkshopReservationByID2Test()
        {
            var result = _workshopsController.GetWorkshopReservationsByID(2);

            var content = Assert.IsAssignableFrom<IEnumerable<ReservationDTO>>(result.Value);
            Assert.Equal(2, content.Count());
        }

        [Fact]
        public void GetWorkshopReservationByID3Test()
        {
            var result = _workshopsController.GetWorkshopReservationsByID(3);

            var content = Assert.IsAssignableFrom<IEnumerable<ReservationDTO>>(result.Value);
            Assert.Equal(2, content.Count());
        }

        [Fact]
        public void PostWorkshopTest()
        {
            var newWorkshop = new Workshop
            {
                Name = "NewWorkshopName",
                Address = "NewWorkshopAddress",
                ProvidedServices = new CarServices
                {
                    ID = 11,
                    TireReplacement = true,
                    AirConCharging = true,
                    PunctureRepair = true,
                    SuspensionAdjustment = true
                },
            };

            var result = _workshopsController.GetWorkshops();
            var content = Assert.IsAssignableFrom<IEnumerable<WorkshopDTO>>(result.Value);
            var beforeCount = content.Count();

            _workshopsController.PostWorkshop((WorkshopDTO)newWorkshop);

            result = _workshopsController.GetWorkshops();
            content = Assert.IsAssignableFrom<IEnumerable<WorkshopDTO>>(result.Value);
            var afterCount = content.Count();

            Assert.Equal(beforeCount, afterCount - 1);
        }

        [Fact]
        public void PostMultipleWorkshopTest()
        {
            var newWorkshop1 = new Workshop
            {
                Name = "NewWorkshopName",
                Address = "NewWorkshopAddress",
                ProvidedServices = new CarServices
                {
                    ID = 11,
                    TireReplacement = true,
                    AirConCharging = true,
                    PunctureRepair = true,
                    SuspensionAdjustment = true
                },
            };

            var newWorkshop2 = new Workshop
            {
                Name = "NewWorkshopName2",
                Address = "NewWorkshopAddress2",
                ProvidedServices = new CarServices
                {
                    ID = 12,
                    TireReplacement = true,
                    AirConCharging = true,
                    PunctureRepair = true,
                    SuspensionAdjustment = true
                },
            };

            var result = _workshopsController.GetWorkshops();
            var content = Assert.IsAssignableFrom<IEnumerable<WorkshopDTO>>(result.Value);
            var beforeCount = content.Count();

            _workshopsController.PostWorkshop((WorkshopDTO)newWorkshop1);
            _workshopsController.PostWorkshop((WorkshopDTO)newWorkshop2);

            result = _workshopsController.GetWorkshops();
            content = Assert.IsAssignableFrom<IEnumerable<WorkshopDTO>>(result.Value);
            var afterCount = content.Count();

            Assert.Equal(beforeCount, afterCount - 2);
        }

        [Fact]
        public void DeleteReservationTest()
        {
            var result = _workshopsController.GetWorkshopReservationsByID(1);
            var contentBefore = Assert.IsAssignableFrom<IEnumerable<ReservationDTO>>(result.Value);

            _reservationController.DeleteReservation(1);

            result = _workshopsController.GetWorkshopReservationsByID(1);
            var contentAfter = Assert.IsAssignableFrom<IEnumerable<ReservationDTO>>(result.Value);

            Assert.Equal(contentAfter.Count()+1, contentBefore.Count());
        }

        [Fact]
        public void DeleteReservationTest2()
        {
            var result = _workshopsController.GetWorkshopReservationsByID(1);
            var contentBefore = Assert.IsAssignableFrom<IEnumerable<ReservationDTO>>(result.Value).ToList();

            _reservationController.DeleteReservation(1);

            result = _workshopsController.GetWorkshopReservationsByID(1);
            var contentAfter = Assert.IsAssignableFrom<IEnumerable<ReservationDTO>>(result.Value).ToList();

            Assert.Equal(contentAfter[0].ID, contentBefore[1].ID);
        }

        [Fact]
        public void DeleteMultipleReservationTest()
        {
            var result = _workshopsController.GetWorkshopReservationsByID(1);
            var contentBefore = Assert.IsAssignableFrom<IEnumerable<ReservationDTO>>(result.Value);

            _reservationController.DeleteReservation(1);
            _reservationController.DeleteReservation(2);

            result = _workshopsController.GetWorkshopReservationsByID(1);
            var contentAfter = Assert.IsAssignableFrom<IEnumerable<ReservationDTO>>(result.Value);

            Assert.Equal(contentAfter.Count() + 2, contentBefore.Count());
        }
    }
}
