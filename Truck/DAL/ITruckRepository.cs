using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Truck.Models;

namespace Truck.DAL
{
    public interface ITruckRepository : IDisposable
    {
        Task<IEnumerable<Models.Truck>> GetTrucks();

        Task<Models.Truck> GetTruck(int? id);

        Task<bool> AddTruck(Models.Truck truck);

        Task<bool> DeleteTruck(int? id);

        bool UpdateTruck(Models.Truck truck);

        void Save();

        Task<ModelType> LoadModel(string modelString);
    }
}
