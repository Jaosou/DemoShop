using DemoShop.Models.db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DemoShop.Controllers
{
    public class PublisherController : Controller
    {
        private readonly DemoShopContext _dbContext;
        public PublisherController(DemoShopContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: PublisherController
        public async Task<ActionResult> Index()
        {
            var pub = from p in _dbContext.Publishers
                      select p;
            return View(await pub.ToArrayAsync());
        }

        // GET: PublisherController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var pub = await _dbContext.Publishers.FirstOrDefaultAsync(c=>c.PublishId == id);
            if (pub == null) { 
                return NotFound();
            }
            return View(pub);
        }

        // GET: PublisherController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PublisherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Publishers.Add(publisher);
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // GET: PublisherController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var pub = await _dbContext.Publishers.FindAsync(id);
            if (pub == null)
            {
                return NotFound();
            }
            return View(pub);
        }

        // POST: PublisherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id,Publisher publisher)

        {
            if (id == 0 )
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Publishers.Update(publisher);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dbContext.Publishers.Any(c => c.PublishId == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: PublisherController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var pub = await _dbContext.Publishers.FindAsync(id);
            if (pub == null)
            {
                return NotFound();
            }
            return View(pub);
        }

        // POST: PublisherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var pub = await _dbContext.Publishers.FindAsync(id);
                if (pub == null)
                {
                    return NotFound();
                }
                _dbContext.Publishers.Remove(pub);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
