using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CompanyController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            
            return View();
        }

       
       public IActionResult Upsert(int? id)
        {
            Company Company = new();
            
			
			if (id== null || id==0) {
                //create company
                return View(Company);
            }
            else //update prpoduct
            {
                Company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
                if (Company == null)
                {
                    return NotFound();
                }
                return View(Company);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {
                
                if(obj.Id != 0)
                {
                    _unitOfWork.Company.Update(obj);
                }
                else
                {
                    _unitOfWork.Company.Add(obj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if(id==0|| id==null)
            {
                return NotFound();
            }
            Company obj = _unitOfWork.Company.GetFirstOrDefault(u=>u.Id == id);
            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {

            Company obj = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            
                _unitOfWork.Company.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product removed successfully";
            return Json(new { success = true, message = "Delete successfully" });
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() {
            var companyList = _unitOfWork.Company.GetAll();
            return Json(new { data = companyList });
        }
		#endregion

	}
}
