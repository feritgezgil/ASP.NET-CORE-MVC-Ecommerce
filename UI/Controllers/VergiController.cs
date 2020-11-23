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
    public class VergiController : Controller
    {
        private readonly ECommerceContext _context;

        public VergiController(ECommerceContext context)
        {
            _context = context;
        }

        // GET: Vergi
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tax.ToListAsync());
        }

        // GET: Vergi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxes = await _context.Tax
                .FirstOrDefaultAsync(m => m.id == id);
            if (taxes == null)
            {
                return NotFound();
            }

            return View(taxes);
        }

        // GET: Vergi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vergi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("value,id,name,c_date,seo_url,is_active,is_delete")] Taxes taxes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taxes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taxes);
        }

        // GET: Vergi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxes = await _context.Tax.FindAsync(id);
            if (taxes == null)
            {
                return NotFound();
            }
            return View(taxes);
        }

        // POST: Vergi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("value,id,name,c_date,seo_url,is_active,is_delete")] Taxes taxes)
        {
            if (id != taxes.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taxes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaxesExists(taxes.id))
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
            return View(taxes);
        }

        // GET: Vergi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxes = await _context.Tax
                .FirstOrDefaultAsync(m => m.id == id);
            if (taxes == null)
            {
                return NotFound();
            }

            return View(taxes);
        }

        // POST: Vergi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taxes = await _context.Tax.FindAsync(id);
            _context.Tax.Remove(taxes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaxesExists(int id)
        {
            return _context.Tax.Any(e => e.id == id);
        }
    }
}
