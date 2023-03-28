using Microsoft.EntityFrameworkCore;
using WePayOffer.DAL.Entity;

namespace WePayOffer.DAL.Database
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            modelBuilder.Entity<Status>().HasData(
                   new Status() { Id = 1, Name = "New" },
                   new Status() { Id = 2, Name = "Succeed" },
                   new Status() { Id = 3, Name = "Failed" },
                   new Status() { Id = 4, Name = "In Progress" }
            );
        }

    }
}
