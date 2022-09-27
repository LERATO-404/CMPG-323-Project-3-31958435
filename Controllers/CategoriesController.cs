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
using DeviceManagement_WebApp.Repository;

namespace DeviceManagement_WebApp.Controllers
{
    public class CategoriesController : Controller
    {
        
        
        private readonly IUnitOfWork _unitOfWork;

       
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // GET: Categories
        public IActionResult Index()
        {
           
            return View(_unitOfWork.CategoryRepository.GetAll());
        }

        // GET: Categories/Details/5
        public IActionResult Details(Guid id)
        {
            bool checkIfCategoryExist = _unitOfWork.CategoryRepository.CheckIfItemExists(id);
            if (checkIfCategoryExist == true)
            {
                return View(_unitOfWork.CategoryRepository.GetById(id));
            }
            return NotFound();


        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CategoryId,CategoryName,CategoryDescription")] Category category)
        {
            
            if (ModelState.IsValid)
            {
                category.CategoryId = Guid.NewGuid();
                _unitOfWork.CategoryRepository.Create(category);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState
                                .Where(x => x.Value.Errors.Count > 0)
                                .Select(x => new { x.Key, x.Value.Errors })
                                .ToArray();
            return View(category);
        }


        // GET: Categories/Edit/5//
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _unitOfWork.CategoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            if (id != category.CategoryId)
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
                    _unitOfWork.CategoryRepository.Edit(id, category);
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_unitOfWork.CategoryRepository.CheckIfItemExists(category.CategoryId) == false)
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _unitOfWork.CategoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            bool checkIfCategoryExist = _unitOfWork.CategoryRepository.CheckIfItemExists(id);
            if (checkIfCategoryExist == true)
            {
                _unitOfWork.CategoryRepository.DeleteConfirmed(id);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
           
        }

        
    }
}
