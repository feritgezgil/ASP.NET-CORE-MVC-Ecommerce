using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UI.Models;
using UI.Models.DAL;
using UI.Models.UI;

namespace UI.Controllers
{
    public class UrunlerController : Controller
    {
        private readonly ECommerceContext _context;

        public UrunlerController(ECommerceContext context)
        {
            _context = context;
        }

        // GET: Urunler
        public async Task<IActionResult> Index()
        {
            var eCommerceContext = _context.Product.Where(x => x.is_active && !x.is_delete).Include(p => p.Menseis).Include(p => p.Taxes);
            var eSideBar = new SideBar(_context);
            eSideBar.AddProductCategories();
            var returnView = Tuple.Create<IEnumerable<Products>, SideBar>(eCommerceContext, eSideBar);
            return View(returnView);
        }

        public async Task<IActionResult> List()
        {
            var eCommerceContext = _context.Product.Include(p => p.Menseis).Include(p => p.Taxes);
            DashboardProduct _dp = new DashboardProduct(eCommerceContext);
            return View(_dp);
        }

        // GET: Urunler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Product
                .Include(p => p.Menseis)
                .Include(p => p.Taxes)
                .FirstOrDefaultAsync(m => m.id == id);
            if (products == null)
            {
                return NotFound();
            }

            var eSideBar = new SideBar(_context);
            eSideBar.AddProductCategories();
            eSideBar.AddSiblingProducts(products.id);
            var returnView = Tuple.Create<Products, SideBar>(products, eSideBar);
            return View(returnView);
        }

        // GET: Urunler/Create
        public IActionResult Create()
        {
            ViewData["mensei_id"] = new SelectList(_context.Mensei, "id", "name");
            ViewData["tax_id"] = new SelectList(_context.Tax, "id", "name");
            ViewData["category_id"] = new SelectList(_context.Category, "id", "name");
             return View();
        }

        // POST: Urunler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("short_desc,desc,is_in_camp,is_in_stock,quantity,tax_id,mensei_id,id,name,is_active")] Products products, string price, string camp_price, string categories)
        {

            //formdan double gelmesi gereken bilgi bölgesel farklılıklar nedeniyle tamsayı olarak dönüşüyor.
            //Alttaki ekle bu durum düzeltiliyor.
            products.price = Tools.toDouble(price);
            products.camp_price = Tools.toDouble(camp_price);
            products.is_delete = false;
            products.c_date = DateTime.Now;
            products.seo_url = Tools.toSlug(products.name);
            if (ModelState.IsValid)
            {
                _context.Add(products);

                _context.SaveChanges();
                List<PrelCs> prelc = new List<PrelCs>();
                foreach (var item in categories.Split(","))
                {
                    prelc.Add(new PrelCs()
                    {
                        ProductId = products.id,
                        CategoryId = Int32.Parse(item)
                    });
                }

                _context.AddRange(prelc);
                _context.SaveChanges();
                FileUploadAndBound(products.id);
            }

            ViewData["mensei_id"] = new SelectList(_context.Mensei, "id", "name", products.mensei_id);
            ViewData["tax_id"] = new SelectList(_context.Tax, "id", "name", products.tax_id);
            ViewData["Categories"] = categories;
            return View(products);
        }

        // GET: Urunler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = _context.Product.Find(id);
            if (products == null)
            {
                return NotFound();
            }
            var prelc = _context.PrelC.Where(x => x.ProductId == products.id).Select(x => x.CategoryId);
            ViewData["mensei_id"] = new SelectList(_context.Mensei, "id", "name", products.mensei_id);
            ViewData["tax_id"] = new SelectList(_context.Tax, "id", "name", products.tax_id);

            var cvm = from item in _context.Category.Where(x => prelc.Contains(x.id))
                      select new CategoryViewModel
                      {
                          Id = item.id,
                          Title = item.name
                      };
            ViewData["Categories"] = cvm;
            return View(products);
        }

        // POST: Urunler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("short_desc,desc,price,camp_price,is_in_camp,is_in_stock,quantity,tax_id,mensei_id,id,name,c_date,seo_url,is_active,is_delete")] Products products, string price, string camp_price, string categories)
        {
            if (id != products.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                products.price = Tools.toDouble(price);
                products.camp_price = Tools.toDouble(camp_price);
                try
                {
                    _context.Update(products);
                    var removedPrelC = _context.PrelC.Where(x => x.ProductId == products.id);
                    _context.RemoveRange(removedPrelC);
                    List<PrelCs> prelc = new List<PrelCs>();
                    foreach (var item in categories.Split(","))
                    {
                        prelc.Add(new PrelCs()
                        {
                            ProductId = products.id,
                            CategoryId = Int32.Parse(item)
                        });
                    }
                    _context.AddRange(prelc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["mensei_id"] = new SelectList(_context.Mensei, "id", "name", products.mensei_id);
            ViewData["tax_id"] = new SelectList(_context.Tax, "id", "name", products.tax_id);
            ViewData["Categories"] = categories;

            return View(products);
        }

        // GET: Urunler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Product
                .Include(p => p.Menseis)
                .Include(p => p.Taxes)
                .FirstOrDefaultAsync(m => m.id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Urunler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Product.FindAsync(id);
            _context.Product.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Product.Any(e => e.id == id);
        }

        public ActionResult SwitchAttribute(int id, string attribute)
        {
            var products = _context.Product
                .Include(p => p.Menseis)
                .Include(p => p.Taxes)
                .FirstOrDefault(m => m.id == id);

            if (ModelState.IsValid)
            {
                try
                {
                    switch (attribute)
                    {
                        case "campaign": products.is_in_camp = !products.is_in_camp; break;
                        case "stock": products.is_in_stock = !products.is_in_stock; break;
                        case "active": products.is_active = !products.is_active; break;
                        case "deleted": products.is_delete = !products.is_delete; break;
                        default:
                            break;
                    }
                    _context.Update(products);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
            }
            return RedirectToAction("List");
        }

        public string GetCategories()
        {
            string result = JsonConvert.SerializeObject(_context.Category);
            return result;
        }

        private List<IFormFile> files = new List<IFormFile>();

        public ActionResult Upload(IFormFile file)
        {
            files.Add(file);
            string jResult = JsonConvert.SerializeObject(file);
            return Json(jResult);
        }

        private JsonResult FileUploadAndBound(int PID)
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            foreach (var file in files)
            {
                try
                {
                    fName = file.FileName;
                    if (file != null && file.Length > 0)
                    {
                        var originalDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fName);

                        using (var stream = new FileStream(originalDirectory, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                    ProductImagess entity = new ProductImagess()
                    {
                        image_url = "/upload/" + file.FileName,
                        name = file.FileName.Split(".")[0],
                        seo_url = Tools.toSlug(file.FileName.Split(".")[0]),
                        is_active = true,
                        is_delete = false,
                        c_date = DateTime.Now,
                        desc = file.FileName.Split(".")[0]
                    };
                    _context.Add(entity);
                    _context.SaveChanges();
                    var temp = new PrelPIs()
                    {
                        ProductId = PID,
                        ProductImageId = entity.id
                    };
                    _context.Add(temp);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    isSavedSuccessfully = false;
                }
            }
            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }
    }
}
