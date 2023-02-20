using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
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
        public IActionResult Create(Category obj)
        {
            //We check for whether the name and the display order of the new category are the same. If so we throw an error.
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Username cannot exactly match the display order");
            }

            //We check for if all inputs are valid. If so we add the category to the database, create a tempdata toastr message
            //for later use and return to the index page.
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            //We check whether id is zero or null since we used a nullable type and if so we throw a not found error.
            if (id == 0 && id == null)
            {
                return NotFound();
            }

            //Then we use Find() method to find the category from the database based on the id since id is the primary key and
            // there is no case where there is more than one element having the same id.
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromFirst = _db.Categories.FirstOrDefault(u=>u.Id = id)
            //var categoryFromSingle = _db.Categories.SingleOrDefault(u=>u.Id = id)
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category obj)
        {
            //We remove the object from the database and create a tempdata error.
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == 0 && id == null)
            {
                return NotFound();
            }

            //var categoryFromFirst = _db.Categories.FirstOrDefault(u=>u.Id = id)
            //var categoryFromSingle = _db.Categories.SingleOrDefault(u=>u.Id = id)
            var categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Username cannot exactly match the display order");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category edited successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
