using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WesternInn_AF_HLN_HP.Model;

namespace WesternInn_AF_HLN_HP.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WesternInn_AF_HLN_HP.Model.Guest> Guest { get; set; }
        public DbSet<WesternInn_AF_HLN_HP.Model.Booking> Booking { get; set; }
        public DbSet<WesternInn_AF_HLN_HP.Model.Room> Room { get; set; }
    }
}