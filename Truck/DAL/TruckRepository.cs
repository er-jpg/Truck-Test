using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Truck.Data;

namespace Truck.DAL
{
    public class TruckRepository : ITruckRepository
    {
        private readonly TruckContext _context;
        private bool disposed = false;

        public TruckRepository(TruckContext context)
        {
            _context = context;
        }

        public async Task<bool> AddTruck(Models.Truck truck)
        {
            if (Enum.TryParse<Models.ModelType.AllowedTypes>(truck.Model.Name, out _))
            {
                await _context.Trucks.AddAsync(truck);
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteTruck(int? id)
        {
            var truck = await GetTruck(id);

            if (truck == null)
            {
                return false;
            }

            _context.Trucks.Remove(truck);
            return true;
        }

        public async Task<Models.Truck> GetTruck(int? id)
        {
            return await _context.Trucks
                .Include(t => t.Model)
                .FirstOrDefaultAsync(t => t.ID == id);
        }

        public async Task<IEnumerable<Models.Truck>> GetTrucks()
        {
            return await _context.Trucks
                .Include(t => t.Model)
                .ToListAsync();
        }

        public async Task<Models.ModelType> LoadModel(string modelString)
        {
            var db = _context.ModelTypes.ToList();

            return await _context.ModelTypes.FirstOrDefaultAsync(m => m.Name == modelString);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool UpdateTruck(Models.Truck truck)
        {
            if (truck == null)
            {
                return false;
            }

            _context.Trucks.Update(truck);
            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
