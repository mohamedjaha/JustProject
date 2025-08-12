using FamilyDataCollector.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FamilyDataCollector.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {
            
        }

        public DbSet<Father> Fathers { get; set; }
        public DbSet<Mother> Mothers { get; set; }
        public DbSet<Child> Childrens { get; set; }
        public DbSet<Family> Families { get; set; }

    }
}
