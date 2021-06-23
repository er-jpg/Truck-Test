using System;
using Truck.Models;
using Xunit;

namespace TruckTest.Models
{
    public class TruckTest
    {
        [Fact]
        public void ConvertViewModelToModel()
        {
            var truck = new Truck.Models.Truck()
            {
                Description = "A Truck for testing",
                FabricationYear = new DateTime(2021, 01, 01),
                ModelYear = new DateTime(2021, 01, 01),
                Name = "Truck Random Name",
                Model = new ModelType()
                {
                    Description = "FH",
                    Name = "FH"
                },
                ID = 1
            };

            var result = truck.MapTruckModel();

            Assert.NotNull(result);
            Assert.Equal("A Truck for testing", result.Description);
            Assert.Equal(2021, result.FabricationYear);
            Assert.Equal(2021, result.ModelYear);
            Assert.Equal("Truck Random Name", result.Name);
        }

        [Fact]
        public void ConvertModelToViewModel()
        {
            var truck = new TruckViewModel()
            {
                Description = "A Truck for testing",
                FabricationYear = 2021,
                ModelYear = 2021,
                Name = "Truck Random Name",
                Model = new ModelType()
                {
                    Description = "FH",
                    Name = "FH"
                },
                ID = 1
            };

            var result = truck.MapTruckViewModel();

            Assert.NotNull(result);
            Assert.Equal("A Truck for testing", result.Description);
            Assert.Equal(2021, result.FabricationYear.Year);
            Assert.Equal(2021, result.ModelYear.Year);
            Assert.Equal("Truck Random Name", result.Name);
        }
    }
}
