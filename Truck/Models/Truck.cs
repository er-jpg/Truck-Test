using System;
using System.ComponentModel.DataAnnotations;

namespace Truck.Models
{
    public class Truck
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public ModelType Model { get; set; }
        public DateTime FabricationYear { get; set; }
        public DateTime ModelYear { get; set; }
    }

    public class TruckViewModel
    {
        public int? ID { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; }

        public ModelType Model { get; set; }

        public string ModelString { get; set; }

        [Required(ErrorMessage = "The fabrication year is required")]
        [Display(Name = "Fabrication Year")]
        public int? FabricationYear { get; set; }

        [Required(ErrorMessage = "The model year is required")]
        [Display(Name = "Model Year")]
        public int? ModelYear { get; set; }
    }

    public static class TruckViewModelExtensions
    {
        public static Truck MapTruckViewModel(this TruckViewModel truck)
        {
            if(IsFabricationYearValid(truck.FabricationYear))
            {
                return null;
            }

            return new Truck
            {
                Description = truck.Description,
                FabricationYear = new DateTime(truck.FabricationYear ?? 1, 1, 1, 1, 1, 1),
                ModelYear = new DateTime(truck.ModelYear ?? 1, 1, 1, 1, 1, 1),
                Model = truck.Model,
                Name = truck.Name,
                ID = truck.ID ?? 0
            };
        }

        public static TruckViewModel MapTruckModel(this Truck truck)
        {
            if (!Enum.TryParse(truck.Model.Name, out ModelType.AllowedTypes type))
            {
                return null;
            }

            if (IsFabricationYearValid(truck.FabricationYear))
            {
                return null;
            }

            return new TruckViewModel
            {
                Description = truck.Description,
                FabricationYear = truck.FabricationYear.Year,
                ModelYear = truck.ModelYear.Year,
                Model = truck.Model,
                Name = truck.Name,
                ID = truck.ID,
                ModelString = type.ToString()
            };
        }

        private static bool IsFabricationYearValid(int? year)
        {
            return !year.Equals(DateTime.Now.Year);
        }

        private static bool IsFabricationYearValid(DateTime truck)
        {
            return !truck.Year.Equals(DateTime.Now.Year);
        }
    }
}
