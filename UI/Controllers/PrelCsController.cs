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
    public class PrelCsController : Controller
    {
        private readonly ECommerceContext _context;

        public PrelCsController(ECommerceContext context)
        {
            _context = context;
        }

        // GET: PrelCs
        public async Task<IActionResult> Index()
        {
            return View(await _context.PrelC.ToListAsync());
        }

        // GET: PrelCs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prelCs = await _context.PrelC
                .FirstOrDefaultAsync(m => m.id == id);
            if (prelCs == null)
            {
                return NotFound();
            }

            return View(prelCs);
        }

        // GET: PrelCs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PrelCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ProductId,CategoryId")] PrelCs prelCs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prelCs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prelCs);
        }

        // GET: PrelCs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prelCs = await _context.PrelC.FindAsync(id);
            if (prelCs == null)
            {
                return NotFound();
            }
            return View(prelCs);
        }

        // POST: PrelCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ProductId,CategoryId")] PrelCs prelCs)
        {
            if (id != prelCs.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prelCs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrelCsExists(prelCs.id))
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
            return View(prelCs);
        }

        // GET: PrelCs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prelCs = await _context.PrelC
                .FirstOrDefaultAsync(m => m.id == id);
            if (prelCs == null)
            {
                return NotFound();
            }

            return View(prelCs);
        }

        // POST: PrelCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prelCs = await _context.PrelC.FindAsync(id);
            _context.PrelC.Remove(prelCs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrelCsExists(int id)
        {
            return _context.PrelC.Any(e => e.id == id);
        }
    }
}
