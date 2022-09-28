using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using DeviceManagement_WebApp.Interfaces;

namespace DeviceManagement_WebApp.Controllers
{
    public class ZonesController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;

        
        public ZonesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Zones
        public IActionResult Index()
        {
            return View(_unitOfWork.ZoneRepository.GetAll());
        }

        // GET: Zones/Details/5
        public IActionResult Details(Guid? id)
        {
            bool checkIfZoneExist = _unitOfWork.ZoneRepository.CheckIfItemExists(id);
            if (checkIfZoneExist == true)
            {
                return View(_unitOfWork.ZoneRepository.GetById(id));
            }
            return NotFound();
        }

        // GET: Zones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zones/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ZoneId,ZoneName,ZoneDescription")] Zone zone)
        {
            
            if (ModelState.IsValid)
            {
                zone.ZoneId = Guid.NewGuid();
                _unitOfWork.ZoneRepository.Create(zone);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState
                                .Where(x => x.Value.Errors.Count > 0)
                                .Select(x => new { x.Key, x.Value.Errors })
                                .ToArray();
            return View(zone);
        }

        // GET: Zones/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _unitOfWork.ZoneRepository.GetById(id);
            if (zone == null)
            {
                return NotFound();
            }
            return View(zone);
        }

        // POST: Zones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            if (id != zone.ZoneId)
            {
                return NotFound();
            }

            var errors = ModelState
                                .Where(x => x.Value.Errors.Count > 0)
                                .Select(x => new { x.Key, x.Value.Errors })
                                .ToArray();
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.ZoneRepository.Edit(zone);
                    _unitOfWork.Save();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.ZoneRepository.CheckIfItemExists(zone.ZoneId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(zone);
        }

        // GET: Zones/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _unitOfWork.ZoneRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Zones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            bool checkIfCategoryExist = _unitOfWork.ZoneRepository.CheckIfItemExists(id);
            if (checkIfCategoryExist == true)
            {
                _unitOfWork.ZoneRepository.DeleteConfirmed(id);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        
    }
}
