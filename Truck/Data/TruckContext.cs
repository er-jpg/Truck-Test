using Microsoft.EntityFrameworkCore;

namespace Truck.Data
{
    public class TruckContext : DbContext
    {
        public TruckContext(DbContextOptions<TruckContext> options) : base(options)
        { }

        public DbSet<Models.ModelType> ModelTypes { get; set; }
        public DbSet<Models.Truck> Trucks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.ModelType>().ToTable("ModelTypes");
            modelBuilder.Entity<Models.Truck>().ToTable("Trucks");
        }
    }
}
