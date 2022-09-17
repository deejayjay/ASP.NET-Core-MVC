using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type added successfully";
                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            var coverTypeFromDb = _unitOfWork.CoverType
                .GetFirstOrDefault(ct => ct.Id == id);

            if (coverTypeFromDb == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDb);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type updated successfully";
                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }

        // GET
        public IActionResult Delete(int? id)
        {
            var coverTypeFromDb = _unitOfWork.CoverType
                .GetFirstOrDefault(ct => ct.Id == id);

            if (coverTypeFromDb == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDb);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var coverTypeFromDb = _unitOfWork.CoverType
                .GetFirstOrDefault(ct => ct.Id == id);

            if (coverTypeFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.CoverType.Remove(coverTypeFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type was deleted successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
