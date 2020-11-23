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
    public class UretimYeriController : Controller
    {
        private readonly ECommerceContext _context;

        public UretimYeriController(ECommerceContext context)
        {
            _context = context;
        }

        // GET: UretimYeri
        public async Task<IActionResult> Index()
        {
            return View(await _context.Mensei.ToListAsync());
        }

        // GET: UretimYeri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menseis = await _context.Mensei
                .FirstOrDefaultAsync(m => m.id == id);
            if (menseis == null)
            {
                return NotFound();
            }

            return View(menseis);
        }

        // GET: UretimYeri/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UretimYeri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("value,id,name,c_date,seo_url,is_active,is_delete")] Menseis menseis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menseis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menseis);
        }

        // GET: UretimYeri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menseis = await _context.Mensei.FindAsync(id);
            if (menseis == null)
            {
                return NotFound();
            }
            return View(menseis);
        }

        // POST: UretimYeri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("value,id,name,c_date,seo_url,is_active,is_delete")] Menseis menseis)
        {
            if (id != menseis.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menseis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenseisExists(menseis.id))
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
            return View(menseis);
        }

        // GET: UretimYeri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menseis = await _context.Mensei
                .FirstOrDefaultAsync(m => m.id == id);
            if (menseis == null)
            {
                return NotFound();
            }

            return View(menseis);
        }

        // POST: UretimYeri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menseis = await _context.Mensei.FindAsync(id);
            _context.Mensei.Remove(menseis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenseisExists(int id)
        {
            return _context.Mensei.Any(e => e.id == id);
        }
    }
}
