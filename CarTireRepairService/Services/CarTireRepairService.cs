using CarTireRepairService.Data;
using CarTireRepairService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarTireRepairService.Services
{
    public class CTRService
    {
        private readonly ApplicationDbContext _context;

        public CTRService(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Workshop

        public List<Workshop> GetWorkshops(String name = null)
        {
            return _context.Workshops
                .Where(w => w.Name.Contains(name ?? ""))
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

        #endregion
    }
}
