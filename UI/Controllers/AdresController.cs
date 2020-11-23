using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UI.Models.DAL;

namespace UI.Controllers
{
    public class AdresController : Controller
    {
        private readonly ECommerceContext _context;
        

        public AdresController(ECommerceContext context)
        {
            _context = context;
        }

        // GET: Adres
        public async Task<IActionResult> Index()
        {
            var eCommerceContext = _context.Address.Include(a => a.Clients);
            return View(await eCommerceContext.ToListAsync());
        }

        // GET: Adres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addresses = await _context.Address
                .Include(a => a.Clients)
                .FirstOrDefaultAsync(m => m.id == id);
            if (addresses == null)
            {
                return NotFound();
            }

            return View(addresses);
        }

        // GET: Adres/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "id", "id");
            return View();
        }

        // POST: Adres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,address,city,country,postcode,addresstype,id,name,c_date,seo_url,is_active,is_delete")] Addresses addresses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addresses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "id", "id", addresses.ClientId);
            return View(addresses);
        }

        // GET: Adres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addresses = await _context.Address.FindAsync(id);
            if (addresses == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "id", "id", addresses.ClientId);
            return View(addresses);
        }

        // POST: Adres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,address,city,country,postcode,addresstype,id,name,c_date,seo_url,is_active,is_delete")] Addresses addresses)
        {
            if (id != addresses.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addresses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressesExists(addresses.id))
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
            ViewData["ClientId"] = new SelectList(_context.Client, "id", "id", addresses.ClientId);
            return View(addresses);
        }

        // GET: Adres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addresses = await _context.Address
                .Include(a => a.Clients)
                .FirstOrDefaultAsync(m => m.id == id);
            if (addresses == null)
            {
                return NotFound();
            }

            return View(addresses);
        }

        // POST: Adres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var addresses = await _context.Address.FindAsync(id);
            _context.Address.Remove(addresses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressesExists(int id)
        {
            return _context.Address.Any(e => e.id == id);
        }
    }
}
