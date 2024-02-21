using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }
        public IActionResult Index()
		{
			List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
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
                _categoryRepo.Add(obj);
                _categoryRepo.Save();
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
			Category? obj = _categoryRepo.Get(u => u.Id == id);
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
                _categoryRepo.Update(obj);
                _categoryRepo.Save();
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
            Category? obj = _categoryRepo.Get(u => u.Id == id);
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
            Category? obj = _categoryRepo.Get(u => u.Id == id);
            
            if (obj == null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(obj);
            _categoryRepo.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");     
        }

    }
}
