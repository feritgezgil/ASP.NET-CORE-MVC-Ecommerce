using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UI.Models.DAL;

namespace UI.Controllers
{
    public class UrunGorselleriController : Controller
    {
        private readonly ECommerceContext _context;

        public UrunGorselleriController(ECommerceContext context)
        {
            _context = context;
        }

        // GET: UrunGorselleri
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductImages.ToListAsync());
        }

        // GET: UrunGorselleri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImagess = await _context.ProductImages
                .FirstOrDefaultAsync(m => m.id == id);
            if (productImagess == null)
            {
                return NotFound();
            }

            return View(productImagess);
        }

        // GET: UrunGorselleri/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UrunGorselleri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("desc,image_url,id,name,c_date,seo_url,is_active,is_delete")] ProductImagess productImagess)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productImagess);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productImagess);
        }

        // GET: UrunGorselleri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImagess = await _context.ProductImages.FindAsync(id);
            if (productImagess == null)
            {
                return NotFound();
            }
            return View(productImagess);
        }

        // POST: UrunGorselleri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("desc,image_url,id,name,c_date,seo_url,is_active,is_delete")] ProductImagess productImagess)
        {
            if (id != productImagess.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productImagess);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductImagessExists(productImagess.id))
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
            return View(productImagess);
        }

        // GET: UrunGorselleri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImagess = await _context.ProductImages
                .FirstOrDefaultAsync(m => m.id == id);
            if (productImagess == null)
            {
                return NotFound();
            }

            return View(productImagess);
        }

        // POST: UrunGorselleri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productImagess = await _context.ProductImages.FindAsync(id);
            _context.ProductImages.Remove(productImagess);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductImagessExists(int id)
        {
            return _context.ProductImages.Any(e => e.id == id);
        }
    }
}
