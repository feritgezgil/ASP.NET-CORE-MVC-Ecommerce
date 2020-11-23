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
    public class EPostaController : Controller
    {
        private readonly ECommerceContext _context;

        public EPostaController(ECommerceContext context)
        {
            _context = context;
        }

        // GET: EPosta
        public async Task<IActionResult> Index()
        {
            var eCommerceContext = _context.Email.Include(e => e.Clients);
            return View(await eCommerceContext.ToListAsync());
        }

        // GET: EPosta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emails = await _context.Email
                .Include(e => e.Clients)
                .FirstOrDefaultAsync(m => m.id == id);
            if (emails == null)
            {
                return NotFound();
            }

            return View(emails);
        }

        // GET: EPosta/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "id", "id");
            return View();
        }

        // POST: EPosta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,emailaddress,id,name,c_date,seo_url,is_active,is_delete")] Emails emails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "id", "id", emails.ClientId);
            return View(emails);
        }

        // GET: EPosta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emails = await _context.Email.FindAsync(id);
            if (emails == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "id", "id", emails.ClientId);
            return View(emails);
        }

        // POST: EPosta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,emailaddress,id,name,c_date,seo_url,is_active,is_delete")] Emails emails)
        {
            if (id != emails.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailsExists(emails.id))
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
            ViewData["ClientId"] = new SelectList(_context.Client, "id", "id", emails.ClientId);
            return View(emails);
        }

        // GET: EPosta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emails = await _context.Email
                .Include(e => e.Clients)
                .FirstOrDefaultAsync(m => m.id == id);
            if (emails == null)
            {
                return NotFound();
            }

            return View(emails);
        }

        // POST: EPosta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emails = await _context.Email.FindAsync(id);
            _context.Email.Remove(emails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailsExists(int id)
        {
            return _context.Email.Any(e => e.id == id);
        }
    }
}
