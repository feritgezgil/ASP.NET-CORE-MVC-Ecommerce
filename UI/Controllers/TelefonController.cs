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
    public class TelefonController : Controller
    {
        private readonly ECommerceContext _context;

        public TelefonController(ECommerceContext context)
        {
            _context = context;
        }

        // GET: Telefon
        public async Task<IActionResult> Index()
        {
            var eCommerceContext = _context.Phone.Include(p => p.Clients);
            return View(await eCommerceContext.ToListAsync());
        }

        // GET: Telefon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phones = await _context.Phone
                .Include(p => p.Clients)
                .FirstOrDefaultAsync(m => m.id == id);
            if (phones == null)
            {
                return NotFound();
            }

            return View(phones);
        }

        // GET: Telefon/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "id", "id");
            return View();
        }

        // POST: Telefon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,phonenumber,id,name,c_date,seo_url,is_active,is_delete")] Phones phones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "id", "id", phones.ClientId);
            return View(phones);
        }

        // GET: Telefon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phones = await _context.Phone.FindAsync(id);
            if (phones == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "id", "id", phones.ClientId);
            return View(phones);
        }

        // POST: Telefon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,phonenumber,id,name,c_date,seo_url,is_active,is_delete")] Phones phones)
        {
            if (id != phones.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhonesExists(phones.id))
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
            ViewData["ClientId"] = new SelectList(_context.Client, "id", "id", phones.ClientId);
            return View(phones);
        }

        // GET: Telefon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phones = await _context.Phone
                .Include(p => p.Clients)
                .FirstOrDefaultAsync(m => m.id == id);
            if (phones == null)
            {
                return NotFound();
            }

            return View(phones);
        }

        // POST: Telefon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phones = await _context.Phone.FindAsync(id);
            _context.Phone.Remove(phones);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhonesExists(int id)
        {
            return _context.Phone.Any(e => e.id == id);
        }
    }
}
