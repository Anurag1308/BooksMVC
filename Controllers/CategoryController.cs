using BooksMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateCategory(Category dto)
        //{

        //}

        public IActionResult Index()
        {
            List<Category> category = _dbContext.Categories.Where(c => c.Status.ToLower() == "active").ToList();
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString().Trim())
            {
                ModelState.AddModelError("Name", "THe name and display order cannot be exactly same.");
            }
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(obj);
                _dbContext.SaveChanges();
                TempData["Success"] = "Category Created Successfully!";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            Category category = _dbContext.Categories.Find(id);
            //Category category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            //Category? category = _dbContext.Categories.Where(c => c.Id == id).FirstOrDefault();

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(obj);
                _dbContext.SaveChanges();
                TempData["Success"] = "Category Updated Successfully!";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        [HttpPost]
        public IActionResult SoftDelete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _dbContext.Categories.Find(id);
            if (category == null)
                return NotFound();

            category.Status = "Inactive";
            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();

            TempData["Success"] = "Category deleted Successfully!";
            return RedirectToAction("Index");
        }
    }
}