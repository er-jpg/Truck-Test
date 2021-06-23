using System;
using System.Linq;
using Xunit;

namespace TruckTest.DAL
{
    public class TruckRepositoryTest
    {
        [Fact]
        public void SaveNewTruck()
        {
            var helper = new TestHelper();

            var repo = helper.GetInMemoryRepository();

            repo.AddTruck(new Truck.Models.Truck()
            {
                Description = "A Truck for testing",
                FabricationYear = new DateTime(2021, 01, 01),
                ModelYear = new DateTime(2021, 01, 01),
                Name = "Truck Random Name",
                Model = new Truck.Models.ModelType()
                {
                    Description = "FH",
                    Name = "FH"
                },
                ID = 1
            });

            repo.Save();

            var result = repo.GetTruck(1).Result;

            Assert.NotNull(result);
            Assert.Equal("A Truck for testing", result.Description);
            Assert.Equal(new DateTime(2021, 01, 01).ToString(), result.FabricationYear.ToString());
            Assert.Equal(new DateTime(2021, 01, 01).ToString(), result.ModelYear.ToString());
            Assert.Equal("Truck Random Name", result.Name);
        }

        [Fact]
        public void FindTruck()
        {
            var helper = new TestHelper();

            var repo = helper.GetInMemoryRepository();

            repo.AddTruck(new Truck.Models.Truck()
            {
                Description = "A Truck for testing",
                FabricationYear = new DateTime(2021, 01, 01),
                ModelYear = new DateTime(2021, 01, 01),
                Name = "Truck Random Name",
                Model = new Truck.Models.ModelType()
                {
                    Description = "FH",
                    Name = "FH"
                },
                ID = 1
            });

            repo.Save();

            var result = repo.GetTruck(1).Result;
            Assert.Equal(1, result.ID);
        }

        [Fact]
        public void UpdateTruck()
        {
            var helper = new TestHelper();

            var repo = helper.GetInMemoryRepository();

            repo.AddTruck(new Truck.Models.Truck()
            {
                Description = "A Truck for testing",
                FabricationYear = new DateTime(2021, 01, 01),
                ModelYear = new DateTime(2021, 01, 01),
                Name = "Truck Random Name",
                Model = new Truck.Models.ModelType()
                {
                    Description = "FH",
                    Name = "FH"
                },
                ID = 1
            });

            repo.Save();

            var truck = repo.GetTruck(1).Result;
            truck.Description = "An Updated Truck for testing";
            truck.Name = "Truck Updated Random Name";

            repo.UpdateTruck(truck);
            repo.Save();

            var result = repo.GetTruck(1).Result;
            Assert.NotNull(result);
            Assert.Equal("An Updated Truck for testing", result.Description);
            Assert.Equal("Truck Updated Random Name", result.Name);
        }

        [Fact]
        public void GetTrucks()
        {
            var helper = new TestHelper();

            var repo = helper.GetInMemoryRepository();

            repo.AddTruck(new Truck.Models.Truck()
            {
                Description = "A Truck for testing",
                FabricationYear = new DateTime(2021, 01, 01),
                ModelYear = new DateTime(2021, 01, 01),
                Name = "Truck Random Name",
                Model = new Truck.Models.ModelType()
                {
                    Description = "FH",
                    Name = "FH"
                },
                ID = 1
            });

            repo.AddTruck(new Truck.Models.Truck()
            {
                Description = "Another Truck for testing",
                FabricationYear = new DateTime(2021, 01, 01),
                ModelYear = new DateTime(2022, 01, 01),
                Name = "Another Truck Random Name",
                Model = new Truck.Models.ModelType()
                {
                    Description = "FM",
                    Name = "FM"
                },
                ID = 2
            });

            repo.Save();

            var result = repo.GetTrucks().Result;
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void DeleteTruck()
        {
            var helper = new TestHelper();

            var repo = helper.GetInMemoryRepository();

            repo.AddTruck(new Truck.Models.Truck()
            {
                Description = "A Truck for testing",
                FabricationYear = new DateTime(2021, 01, 01),
                ModelYear = new DateTime(2021, 01, 01),
                Name = "Truck Random Name",
                Model = new Truck.Models.ModelType()
                {
                    Description = "FH",
                    Name = "FH"
                },
                ID = 1
            });

            repo.Save();

            repo.DeleteTruck(1).ConfigureAwait(false);
            repo.Save();

            var result = repo.GetTruck(1).Result;
            Assert.Null(result);
        }
    }
}
