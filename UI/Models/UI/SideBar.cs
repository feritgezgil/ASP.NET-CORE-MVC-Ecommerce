using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UI.Models.DAL;

namespace UI.Models.UI
{
    public class SideBar
    {

        public List<Menus> menus { get; set; }
        private ECommerceContext context;
        public SideBar(ECommerceContext _context)
        {
            context = _context;
            menus = context.Menu.Include(s => s.MenuItems).ToList();
        }

        public void AddProductCategories()
        {
            Menus pCat = new Menus
            {
                name = "Ürün Kategorileri",
                is_icon_menu = false,
                is_active = true,
                seo_url = "urun-kategorileri",
                MenuItems = new List<MenuItems>()
            };
            foreach (var item in context.Category.ToList())
            {
                pCat.MenuItems.Add(
                    new MenuItems()
                    {
                        name = item.name,
                        url = item.seo_url,
                        seo_url = item.seo_url
                    }
                    );
            }
            menus.Add(pCat);
        }
        public void AddSiblingProducts(int id, int count = 3) {
            Products p = context.Product.Find(id);
            Menus pCat = new Menus
            {
                name = "Benzer Ürünler",
                is_icon_menu = false,
                is_active = true,
                seo_url = "urun-kategorisi",
                MenuItems = new List<MenuItems>()
            };
            foreach (var item in context.Product.Where(x=>x.id!=id).Take(count).ToList())
            {
                pCat.MenuItems.Add(
                    new MenuItems()
                    {
                        name = item.name,
                        url = "/urunler/details/"+item.id,
                        seo_url = "/urunler/details/" + item.id
                    }
                    );
            }
            menus.Add(pCat);
        }
    }
}
