using DemoShop.Models.db;
using Microsoft.AspNetCore.Mvc;

namespace DemoShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DemoShopContext _dbContext;

        public CategoryController(DemoShopContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var allc = from c in _dbContext.Categories
                       select c;
            if (allc == null)
            {
                return NotFound();
            }
            return View(allc);
        }

        public IActionResult Create(Category category)
        {
            //check if the model is valid (Havw input data?)
            if (ModelState.IsValid)
            {   
                //true
                //Add the category to the database
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                //back to index
                return RedirectToAction("Index");   
            }
            return View(category);
        }

        public IActionResult Update()
        {
            IEnumerable<Category> allCate = _dbContext.Categories;
            return View(allCate);
        }

        public IActionResult ShowUpd(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public IActionResult UpdateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(category);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult deleteCategory(int? id)
        {
            if (id == null || id == 0 )
            {
                return NotFound();
            }
            var category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
