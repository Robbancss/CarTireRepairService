using CarTireRepairService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarTireRepairService.Data
{
    public class ApplicationDbContext : IdentityDbContext<Client>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Workshop> Workshops { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<CarServices> ProvidedServices { get; set; }
    }
}
