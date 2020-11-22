using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CarTireRepairService.Models;
using CarTireRepairService.Services;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace CarTireRepairService.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly CTRService _service;
        private readonly UserManager<Client> _userManager;
        private IPagedList<DateTime> _dateTimes;

        private Reservation _inProgressReservation;


        public ReservationsController(CTRService service, UserManager<Client> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        // GET: Reservations
        public ViewResult Index(Int32? ID, ServiceTypes serviceType, Int32? page)
        {
            if (ID == null)
            {
                return View();
            }
            var temp = Enumerable.Range(00, 08).Select(i => Enumerable.Range(0, 2).Select(j => new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, i + 8, j * 30, 0)).ToList()).ToList();
            var reservations = _service.GetReservationsByWorkshopID((int)ID);
            List<DateTime> dateTimes = new List<DateTime>();
            for (int i = 0; i < 7; i++)
            {
                foreach (var elem in temp)
                {
                    foreach (var twoelem in elem)
                    {
                        var withAddedDay = twoelem.AddDays(i);
                        dateTimes = dateTimes.Append(withAddedDay).ToList();
                    }
                }
            }

            foreach (var reservation in reservations)
            {
                dateTimes = dateTimes.Where(d => d != reservation.ReservationTime).ToList();
            }
            ViewBag.ReservationTimes = dateTimes;

            var pageNumber = page ?? 1;
            _dateTimes = dateTimes.ToPagedList(pageNumber, 10);

            //ViewBag.OnePageAvailableReservations = onePageAvailableReservations;
            ViewBag.ID = ID;
            ViewBag.ServiceType = serviceType;

            return View(_dateTimes);
        }

        public IActionResult Create(Int32? ID, ServiceTypes serviceType, string chosenDateTime)
        {
            if (ID == null)
            {
                return NotFound();
            }

            try
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                var chosenDate = DateTime.ParseExact(chosenDateTime, "yyyyMMddHHmmss", provider);

                _inProgressReservation = new Reservation();
                _inProgressReservation.ProvidedService = new CarServices();
                _inProgressReservation.UserID = _userManager.GetUserId(User);
                _inProgressReservation.ReservationTime = chosenDate;
                _inProgressReservation.Workshop = _service.GetWorkShopByID((int)ID);
                switch (serviceType)
                {
                    case ServiceTypes.TireReplacement:
                        _inProgressReservation.ProvidedService.TireReplacement = true;
                        break;
                    case ServiceTypes.AirConCharging:
                        _inProgressReservation.ProvidedService.AirConCharging = true;
                        break;
                    case ServiceTypes.PunctureRepair:
                        _inProgressReservation.ProvidedService.PunctureRepair = true;
                        break;
                    case ServiceTypes.SuspensionAdjustment:
                        _inProgressReservation.ProvidedService.SuspensionAdjustment = true;
                        break;
                    default:
                        break;
                }

                ViewBag.ChosenShop = _inProgressReservation.Workshop.Name;
                ViewBag.ServiceType = serviceType;
                ViewBag.ChosenDate = _inProgressReservation.ReservationTime;

                HttpContext.Session.SetString(_service.SShopID, ID.ToString());
                HttpContext.Session.SetString(_service.SServiceType, serviceType.ToString());
                HttpContext.Session.SetString(_service.STime, chosenDate.ToString("yyyMMddHHmmss"));

                return View(_inProgressReservation);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var shopID = HttpContext.Session.GetString(_service.SShopID);
            
            if (shopID == null)
            {
                return NotFound();
            }

            var serviceType = (ServiceTypes)Enum.Parse(typeof(ServiceTypes), HttpContext.Session.GetString(_service.SServiceType));
            var time = DateTime.ParseExact(HttpContext.Session.GetString(_service.STime), "yyyyMMddHHmmss", provider);

            CarServices st = new CarServices();

            switch (serviceType)
            {
                case ServiceTypes.TireReplacement:
                    st.TireReplacement = true;
                    break;
                case ServiceTypes.AirConCharging:
                    st.AirConCharging = true;
                    break;
                case ServiceTypes.PunctureRepair:
                    st.PunctureRepair = true;
                    break;
                case ServiceTypes.SuspensionAdjustment:
                    st.SuspensionAdjustment = true;
                    break;
                default:
                    break;
            }

            var shop = _service.GetWorkShopByID(Int32.Parse(shopID));

            if (shop == null)
            {
                return NotFound();
            }

            Reservation reservation = new Reservation();
            reservation.ReservationTime = time;
            reservation.UserID = _userManager.GetUserId(User);
            reservation.ProvidedService = st;

            foreach (var res in shop.Reservations)
            {
                if (res.ReservationTime == reservation.ReservationTime &&
                    res.Workshop.ID == reservation.Workshop.ID &&
                    res.ProvidedService == reservation.ProvidedService)
                {
                    return RedirectToAction("index", "home");
                }
            }

            shop.Reservations.Add(reservation);

            if (_service.UpdateWorkshop(shop))
            {
                return RedirectToAction("index", "home");
            }

            return View();
        }
    }
}
