using System.Collections.Generic;
using System.Linq;

namespace Truck.Data
{
    public class DbInitializer
    {
        public static void Initialize(TruckContext context)
        {
            context.Database.EnsureCreated();

            if (context.ModelTypes.Any())
            {
                return;
            }

            var allowedModels = new List<Models.ModelType>
            {
                new Models.ModelType{ Name="FH", Description="Linha para fretes de longa distância" },
                new Models.ModelType{ Name="FM", Description="Linha para fretes regionais e de média distância." }
            };

            foreach(var am in allowedModels)
            {
                context.Add(am);
                context.SaveChanges();
            }
        }
    }
}
