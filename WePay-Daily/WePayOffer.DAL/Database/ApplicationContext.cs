using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WePayOffer.DAL.Entity;
using WePayOffer.DAL.Extend;

namespace WePayOffer.DAL.Database
{
    public partial class ApplicationContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt)
        {

        }

        // Seed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new DbInitializer(modelBuilder).Seed();
            base.OnModelCreating(modelBuilder);
        }
    }

    public partial class ApplicationContext
    {
        public DbSet<Status> Status { get; set; }
        public DbSet<ServiceNumber> ServiceNumber { get; set; }
        public DbSet<ServiceTransaction> ServiceTransaction { get; set; }

    }
}
