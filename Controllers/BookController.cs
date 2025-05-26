using System.Security.Policy;
using System.Text.Json;  // สำหรับ JsonSerializer
using DemoShop.Models.db;
using DemoShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DemoShop.Controllers
{
    public class BookController : Controller
    {
        private readonly DemoShopContext _dbContext;
        public BookController(DemoShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResult> Search(string q = "")
        {
            var bcp = (from b in _dbContext.Books
                       from c in _dbContext.Categories
                       from p in _dbContext.Publishers
                       where (b.BookName.Contains(q))
                       && (b.CategoryId == c.CategoryId)
                       && (b.PublishId == p.PublishId)
                       select new BookCategoryPublisherViewModel
                       {
                           BookId = b.BookId,
                           BookName = b.BookName,
                           CategoryName = c.CategoryName,
                           PublishName = p.PublishName,
                           BookCost = b.BookCost,
                           BookPrice = b.BookPrice ?? 0.0
                       }).ToListAsync();
            if (bcp != null)
            {
                return View(await bcp);
            }
            return NotFound();
        }

        public async Task<ActionResult> IndexViewModel()
        {
            var bcp = from b in _dbContext.Books
                      from c in _dbContext.Categories
                      from p in _dbContext.Publishers
                      where (b.CategoryId == c.CategoryId)
                      && (b.PublishId == p.PublishId)
                      select new BookCategoryPublisherViewModel
                      {
                          BookId = b.BookId,
                          BookName = b.BookName,
                          CategoryName = c.CategoryName,
                          PublishName = p.PublishName,
                          BookCost = b.BookCost,
                          BookPrice = b.BookPrice ?? 0.0 //ถ้ากรณี BookPrice เป็น null ให้ใช้ค่า 0.0 (หรือค่าอื่นตามที่ต้องการ)
                      };
            return View(await bcp.ToListAsync());
        }

        // GET: BookController
        public async Task<ActionResult> Index()
        {
            var books = _dbContext.Books.Include(c => c.Category).Include(p => p.Publish);
            if (books == null)
            {
                return NotFound();
            }
            return View(await books.ToListAsync());
        }

        // GET: BookController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var book = await _dbContext.Books
                .Include(c => c.Category)
                .Include(p => p.Publish)
                .FirstOrDefaultAsync(b=>b.BookId ==  id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: BookController/Create
        [HttpGet]
        public ActionResult Create()
        {
            var categories = _dbContext.Categories.ToList();
            var publishers = _dbContext.Publishers.ToList();

            Console.WriteLine(JsonSerializer.Serialize(categories));
            Console.WriteLine(JsonSerializer.Serialize(publishers));

            ViewData["CategoryId"]= new SelectList(categories, "CategoryId", "CategoryName");
            ViewData["PublishId"] = new SelectList(publishers, "PublishId", "PublishName");
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("BookId,BookName,CategoryId,PublishId,Isbn,BookCost,BookPrice")]
            Book b, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState
        )
        {
            if (!modelState.IsValid) {
                ViewData["CategoryId"] = new SelectList(_dbContext.Categories, "CategoryId", "CategoryName");
                ViewData["PublishId"] = new SelectList(_dbContext.Publishers, "PublishId", "PublishName");
            }
            b.BookId = Guid.NewGuid().ToString();
            _dbContext.Books.Add(b);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: BookController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_dbContext.Categories, "CategoryId", "CategoryName");
            ViewData["PublishId"] = new SelectList(_dbContext.Publishers, "PublishId", "PublishName");
            return View(book);
        }

        private bool BookExists(string id)
        {
            return _dbContext.Books.Any(e => e.BookId == id);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, [Bind("BookId,BookName,CategoryId,PublishId,Isbn,BookCost,BookPrice")]
            Book b, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
        {
            if (id != b.BookId)
            {
                return NotFound();
            }
            if (modelState.IsValid)
            {
                try
                {
                    _dbContext.Update(b);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (BookExists(b.BookId))
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
            ViewData["CategoryId"] = new SelectList(_dbContext.Categories, "CategoryId", "CategoryName");
            ViewData["PublishId"] = new SelectList(_dbContext.Publishers, "PublishId", "PublishName");
            return View(b);
        }

        // GET: BookController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _dbContext.Books
                .Include(c => c.Category)
                .Include(p => p.Publish)
                .FirstOrDefaultAsync(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfrimed(string id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
