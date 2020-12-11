using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.Services
{
    public class CTRService
    {
        private readonly ApplicationDbContext _context;

        private const string SessionSavedResTime = "_restime";
        private const string SessionSavedShopID = "_shopid";
        private const string SessionSavedServiceType = "_servicetype";

        public CTRService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string STime
        {
            get { return SessionSavedResTime; }
        }

        public string SShopID
        {
            get { return SessionSavedShopID; }
        }

        public string SServiceType
        {
            get { return SessionSavedServiceType; }
        }

        #region Workshops

        public List<Workshop> GetWorkshops(String name = null)
        {
            return _context.Workshops
                .Where(w => (w.Name.Contains(name ?? "") || w.Address.Contains(name ?? "")))
                .Include(w => w.ProvidedServices)
                .OrderBy(w => w.Name)
                .ToList<Workshop>();
        }

        public List<Workshop> GetWorkshopsByStreet(String street = null)
        {
            return _context.Workshops
                .Where(w => w.Address.Contains(street ?? ""))
                .Include(w => w.ProvidedServices)
                .OrderBy(w => w.Name)
                .ToList<Workshop>();
        }

        public Workshop GetWorkShopByID(Int32 ID)
        {
            return _context.Workshops
                .Include(w => w.Reservations)
                .Include(w => w.ProvidedServices)
                .Single(w => w.ID == ID);
        }

        public bool UpdateWorkshop(Workshop workshop)
        {
            try
            {
                _context.Update(workshop);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Reservations

        public List<Reservation> GetReservationsByWorkshopID(Int32 workshopID)
        {
            return _context.Reservations
                .Where(r => r.Workshop.ID == workshopID)
                .Include(r => r.Workshop)
                .ToList<Reservation>();
        }

        public List<Reservation> GetReservationsByUserID(Int32 UserID)
        {
            return _context.Reservations
                .Where(r => r.UserID == UserID)
                .Include(r => r.Workshop)
                .Include(r => r.ProvidedService)
                .ToList<Reservation>();
        }

        #endregion
    }
}
