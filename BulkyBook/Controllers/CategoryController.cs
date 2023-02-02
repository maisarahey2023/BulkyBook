using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _db;
        public CategoryController(IUnitOfWork db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Category.GetAll();
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if(category.Name == category.DisplayOrder.ToString()) {
                ModelState.AddModelError("name", "The display order cannot exactly match the name");
            }
            if(ModelState.IsValid)
            {
                _db.Category.Add(category);
                _db.Save();
				TempData["success"] = "The Category has been added successfully!";
				return RedirectToAction("Index");
            }
            return View(category);
        }

		//GET
		public IActionResult Edit(int? id)
		{
            //verify id
            if(id==null || id == 0)
            {
                return NotFound();
            }

            //find the category using id and verify
            var categoryFromDb = _db.Category.GetFirstOrDefault(u => u.Id == id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }

            //return the category to edit view
			return View(categoryFromDb);
		}
		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category category)
		{
			if (category.Name == category.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "The display order cannot exactly match the name");
			}
			if (ModelState.IsValid)
			{
				_db.Category.Update(category);
				_db.Save();
				TempData["success"] = "The Category has been updated successfully!";
				return RedirectToAction("Index");
			}
			return View(category);
		}

        //GET
        public IActionResult Delete(int? id)
        {
            //verify id
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //find the category using id and verify
            var categoryFromDb = _db.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            //return the category to edit view
            return View(categoryFromDb);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var categoryFromDb = _db.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Category.Remove(categoryFromDb);
                _db.Save();
                TempData["success"] = "The Category has been removed successfully!";
                return RedirectToAction("Index");
            }
            return View(categoryFromDb);
        }
    }
}
