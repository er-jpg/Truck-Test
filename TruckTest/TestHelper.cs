using Microsoft.EntityFrameworkCore;
using Truck.DAL;
using Truck.Data;

namespace TruckTest
{
    public class TestHelper
    {
        private readonly TruckContext truckContext;
        public TestHelper()
        {
            var builder = new DbContextOptionsBuilder<TruckContext>();
            builder.UseInMemoryDatabase(databaseName: "TruckDbInMemory");

            var dbContextOptions = builder.Options;
            truckContext = new TruckContext(dbContextOptions);
            truckContext.Database.EnsureDeleted();
            truckContext.Database.EnsureCreated();
        }

        public ITruckRepository GetInMemoryRepository()
        {
            return new TruckRepository(truckContext);
        }
    }
}
