using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext<Client, IdentityRole<int>, int>
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
