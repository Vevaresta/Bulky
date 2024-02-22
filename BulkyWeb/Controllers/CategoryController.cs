﻿using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
	public class CategoryController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
		{
			List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
			return View(objCategoryList);
		}
		// If I don't define anything it is by default GET Method
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
        public IActionResult Create(Category obj)
        {
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
			}

			if (ModelState.IsValid)
			{
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
				return RedirectToAction("Index");
			}
			return View();
        }

        public IActionResult Edit(int? id)
        {
			if(id==null || id == 0)
			{
				return NotFound();
			}
			Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            //Category? obj = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? obj = _db.Categories.Where(u=>u.Id==id)FirstOrDefault();

            if (obj == null) 
			{ 
				return NotFound(); 
			}
            
			return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                 TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            //Category? obj = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? obj = _db.Categories.Where(u=>u.Id==id)FirstOrDefault();

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");     
        }

    }
}
