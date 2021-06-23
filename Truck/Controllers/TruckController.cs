using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Truck.DAL;
using Truck.Models;

namespace Truck.Controllers
{
    public class TruckController : Controller
    {
        private readonly ILogger<TruckController> _logger;
        private readonly ITruckRepository _truckRepository;

        public TruckController(ILogger<TruckController> logger,
            ITruckRepository truckRepository)
        {
            _logger = logger;
            _truckRepository = truckRepository;
        }

        public async Task<IActionResult> Index()
        {
            var trucks = await _truckRepository.GetTrucks();
            var trucksViewModel = new List<TruckViewModel>();

            foreach(var truck in trucks)
            {
                trucksViewModel.Add(truck.MapTruckModel());
            }

            return View(trucksViewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truck = await _truckRepository.GetTruck(id);

            if (truck == null)
            {
                return NotFound();
            }

            return View(truck.MapTruckModel());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TruckViewModel truck)
        {
            if (ModelState.IsValid)
            {
                var truckModel = truck.MapTruckViewModel();

                if(truckModel is null)
                {
                    ModelState.AddModelError(string.Empty, "Cannot add truck!");
                    return View(truck);
                }

                truckModel.Model = await _truckRepository.LoadModel(((ModelType.AllowedTypes)Convert.ToInt32(truck.ModelString)).ToString());

                if(truckModel.Model is null)
                {
                    ModelState.AddModelError(string.Empty, "Model was not found!");
                    return View(truck);
                }

                if (await _truckRepository.AddTruck(truckModel))
                {
                    _truckRepository.Save();
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("Error", "There was an error adding new truck!");
            }
            return View(truck);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truck = await _truckRepository.GetTruck(id);
            if (truck == null)
            {
                return NotFound();
            }
            return View(truck.MapTruckModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TruckViewModel truck)
        {
            if (id != truck.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var truckModel = truck.MapTruckViewModel();

                truckModel.Model = await _truckRepository.LoadModel(((ModelType.AllowedTypes)Convert.ToInt32(truck.ModelString)).ToString());

                if (_truckRepository.UpdateTruck(truckModel))
                {
                    _truckRepository.Save();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(truck);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truck = await _truckRepository.GetTruck(id);
            if (truck == null)
            {
                return NotFound();
            }

            return View(truck.MapTruckModel());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (await _truckRepository.DeleteTruck(id))
            {
                _truckRepository.Save();
                return RedirectToAction(nameof(Index));
            }

            var truck = await _truckRepository.GetTruck(id);
            return RedirectToAction(nameof(Delete), truck);
        }
    }
}
