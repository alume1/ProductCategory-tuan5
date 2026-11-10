using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductCategory_MVC.Data;
using ProductCategory_MVC.Models;

namespace ProductCategory_MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var list = await (from p in _context.Products
                              join c in _context.Categories on p.categoryId equals c.categoryId
                              select new Productviewmodel
                              {
                                  productId = p.productId,
                                  productName = p.productName,
                                  productPrice = p.productPrice,
                                  Description = p.Description,
                                  categoryName = c.categoryName
                              }).ToListAsync();

            return View(list);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await (from p in _context.Products
                                 join c in _context.Categories on p.categoryId equals c.categoryId
                                 where p.productId == id
                                 select new Productviewmodel
                                 {
                                     productId = p.productId,
                                     productName = p.productName,
                                     productPrice = p.productPrice,
                                     Description = p.Description,
                                     categoryName = c.categoryName
                                 }).FirstOrDefaultAsync();

            if (product == null) return NotFound();
            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "categoryId", "categoryName");
            return View();
        }

        // POST: Product/Create
        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            ModelState.Remove("Category");
            product.Category = null;

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(_context.Categories, "categoryId", "categoryName", product.categoryId);
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(_context.Categories, "categoryId", "categoryName", product.categoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.productId) return NotFound();

            ModelState.Remove("Category");
            product.Category = null;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Products.Any(e => e.productId == id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(_context.Categories, "categoryId", "categoryName", product.categoryId);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await (from p in _context.Products
                                 join c in _context.Categories on p.categoryId equals c.categoryId
                                 where p.productId == id
                                 select new Productviewmodel
                                 {
                                     productId = p.productId,
                                     productName = p.productName,
                                     productPrice = p.productPrice,
                                     Description = p.Description,
                                     categoryName = c.categoryName
                                 }).FirstOrDefaultAsync();

            if (product == null) return NotFound();
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}