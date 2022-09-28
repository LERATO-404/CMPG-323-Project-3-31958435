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
    public class DevicesController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;

        
        public DevicesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Devices
        public IActionResult Index()
        {
            
            return View(_unitOfWork.DeviceRepo.GetAlldevice());
        }

        // GET: Devices/Details/5
        public IActionResult Details(Guid? id)
        {

            return View(_unitOfWork.DeviceRepo.GetDeviceById(id));
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_unitOfWork.DeviceRepo.GetCategory(), "CategoryId", "CategoryName");
            ViewData["ZoneId"] = new SelectList(_unitOfWork.DeviceRepo.GetZone(), "ZoneId", "ZoneName");
            return View();
        }

        // POST: Devices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive")] Device device)
        {
            if (ModelState.IsValid)
            {
                device.DeviceId = Guid.NewGuid();
                _unitOfWork.DeviceRepository.Create(device);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_unitOfWork.DeviceRepo.GetCategory(), "CategoryId", "CategoryName", device.CategoryId);
            ViewData["ZoneId"] = new SelectList(_unitOfWork.DeviceRepo.GetZone(), "ZoneId", "ZoneName", device.ZoneId);

            var errors = ModelState
                                .Where(x => x.Value.Errors.Count > 0)
                                .Select(x => new { x.Key, x.Value.Errors })
                                .ToArray();
            return View(device);
        }

        // GET: Devices/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = _unitOfWork.DeviceRepo.GetDeviceById(id);
            if (device == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_unitOfWork.DeviceRepo.GetCategory(), "CategoryId", "CategoryName", device.CategoryId);
            ViewData["ZoneId"] = new SelectList(_unitOfWork.DeviceRepo.GetZone(), "ZoneId", "ZoneName", device.ZoneId);
            return View(device);
        }

        // POST: Devices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            if (id != device.DeviceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DeviceRepository.Edit(device);
                    _unitOfWork.Save();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.DeviceRepository.CheckIfItemExists(device.DeviceId))
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
            ViewData["CategoryId"] = new SelectList(_unitOfWork.DeviceRepo.GetCategory(), "CategoryId", "CategoryName", device.CategoryId);
            ViewData["ZoneId"] = new SelectList(_unitOfWork.DeviceRepo.GetZone(), "ZoneId", "ZoneName", device.ZoneId);
            return View(device);
        }

        // GET: Devices/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = _unitOfWork.DeviceRepo.GetDeviceById(id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _unitOfWork.DeviceRepository.DeleteConfirmed(id);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
