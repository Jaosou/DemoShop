using System.Security.Policy;
using DemoShop.Models.db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DemoShop.Controllers
{
    public class CustomerController : Controller
    {
        private readonly DemoShopContext _dbContext;
        public CustomerController(DemoShopContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: CustomerController
        public async Task<ActionResult> Index()
        {
            var customers = from c in _dbContext.Customers select c;
            return View(await customers.ToArrayAsync());
        }

        // GET: CustomerController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // GET: CustomerController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Customer customer)
        {
            customer.RegistrationDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Customers.Add(customer);
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: CustomerController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Customer customer)
        {
            if (id == 0)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Customers.Update(customer);
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {
                    if (!_dbContext.Customers.Any(c => c.CustomerId == id))
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

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
