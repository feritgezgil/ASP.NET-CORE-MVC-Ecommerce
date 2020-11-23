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
    public class PrelPIsController : Controller
    {
        private readonly ECommerceContext _context;

        public PrelPIsController(ECommerceContext context)
        {
            _context = context;
        }

        // GET: PrelPIs
        public async Task<IActionResult> Index()
        {
            return View(await _context.PrelPI.ToListAsync());
        }

        // GET: PrelPIs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prelPIs = await _context.PrelPI
                .FirstOrDefaultAsync(m => m.id == id);
            if (prelPIs == null)
            {
                return NotFound();
            }

            return View(prelPIs);
        }

        // GET: PrelPIs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PrelPIs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ProductId,ProductImageId")] PrelPIs prelPIs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prelPIs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prelPIs);
        }

        // GET: PrelPIs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prelPIs = await _context.PrelPI.FindAsync(id);
            if (prelPIs == null)
            {
                return NotFound();
            }
            return View(prelPIs);
        }

        // POST: PrelPIs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ProductId,ProductImageId")] PrelPIs prelPIs)
        {
            if (id != prelPIs.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prelPIs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrelPIsExists(prelPIs.id))
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
            return View(prelPIs);
        }

        // GET: PrelPIs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prelPIs = await _context.PrelPI
                .FirstOrDefaultAsync(m => m.id == id);
            if (prelPIs == null)
            {
                return NotFound();
            }

            return View(prelPIs);
        }

        // POST: PrelPIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prelPIs = await _context.PrelPI.FindAsync(id);
            _context.PrelPI.Remove(prelPIs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrelPIsExists(int id)
        {
            return _context.PrelPI.Any(e => e.id == id);
        }
    }
}
