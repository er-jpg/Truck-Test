using System.Collections.Generic;

namespace Truck.Models
{
    public class ModelType
    {
        public enum AllowedTypes
        {
            FH = 1,
            FM = 2
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Truck> Trucks { get; set; }
    }
}
