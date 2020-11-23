using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UI.Models.DAL;

namespace UI.Models.UI
{
    public class ProductListItemViewModel : BaseViewModel
    {
        [Display(Name = "Price")]
        public double price { get; set; }

        [Display(Name = "Campaign Price")]
        public double camp_price { get; set; }

        [Display(Name = "Is In Campaign?")]
        public bool is_in_camp { get; set; }

        [Display(Name = "Is In Stock?")]
        public bool is_in_stock { get; set; }

        [Display(Name = "Quantity")]
        public int quantity { get; set; }
    }
    public class ProductDetailViewModel : BaseViewModel
    {
        [Display(Name = "Create Date")]
        public DateTime c_date { get; set; }

        [Display(Name = "SEO Friendly URL")]
        public string seo_url { get; set; }

        [Display(Name = "Short Description")]
        public string short_desc { get; set; }

        [Display(Name = "Description")]
        public string desc { get; set; }

        [Display(Name = "Price")]
        public double price { get; set; }

        [Display(Name = "Campaign Price")]
        public double camp_price { get; set; }

        [Display(Name = "Is In Campaign?")]
        public bool is_in_camp { get; set; }

        [Display(Name = "Is In Stock?")]
        public bool is_in_stock { get; set; }

        [Display(Name = "Quantity")]
        public int quantity { get; set; }

        public int tax_id { get; set; }

        [Display(Name = "Taxes")]
        public IEnumerable<TaxViewModel> taxes { get; set; }

        public int mensei_id { get; set; }

        [Display(Name = "Menşeis")]
        public IEnumerable<MenseiViewModel> menseis { get; set; }

        [Display(Name = "Categories")]
        public IEnumerable<CategoryViewModel> categories { get; set; }

        [Display(Name = "Product Images")]
        public IEnumerable<ProductImageViewModel> productImages { get; set; }
    }

    public class TaxViewModel
    {

    }

    public class MenseiViewModel
    {

    }

    public class CategoryViewModel
    {
        private int id;
        private string title;

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
    }

    public class ProductImageViewModel
    {

    }

    public class ProductAttributeCards
    {
        public ProductAttribute[] productAttributes { get; set; }
        public ProductAttributeCards(IEnumerable<ProductListItemViewModel> products)
        {
            productAttributes = new ProductAttribute[5];

            productAttributes[0] = new ProductAttribute()
            {
                Title = "Kayıtlı Ürünler",
                Icon = "fas fa-cart-arrow-down",
                Style = "warning",
                Value = products.Count()
            };

            productAttributes[1] = new ProductAttribute()
            {
                Title = "Kampanyalı Ürünler",
                Icon = "fas fa-tags",
                Style = "danger",
                Value = products.Count(x => x.is_in_camp)
            };

            productAttributes[2] = new ProductAttribute()
            {
                Title = "Stoklu Ürünler",
                Icon = "fab fa-stack-overflow",
                Style = "info",
                Value = products.Count(x => x.is_in_stock)
            };

            productAttributes[3] = new ProductAttribute()
            {
                Title = "Satıştaki Ürünler",
                Icon = "fas fa-check",
                Style = "success",
                Value = products.Count(x => x.is_active)
            };

            productAttributes[4] = new ProductAttribute()
            {
                Title = "Kaldırılmış Ürünler",
                Icon = "fas fa-recycle",
                Style = "dark",
                Value = products.Count(x => x.is_delete)
            };
        }
    }

    public class ProductAttribute
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Style { get; set; }
        public int Value { get; set; }
        public bool progressType { get; set; }
    }

    public class DashboardProduct
    {
        public List<ProductAttribute> Attributes { get; set; }
        public List<ProductListItemViewModel> ProductTable { get; set; }
        public DashboardProduct(IEnumerable<Products> products)
        {
            ProductTable = new List<ProductListItemViewModel>();
            foreach (var item in products)
            {
                ProductTable.Add(new ProductListItemViewModel
                {
                    id = item.id,
                    name = item.name,
                    is_active = item.is_active,
                    is_delete = item.is_delete,
                    price = item.price,
                    camp_price = item.camp_price,
                    is_in_camp = item.is_in_camp,
                    is_in_stock = item.is_in_stock,
                    quantity = item.quantity
                });
            }
            ProductAttributeCards productAttributeCards = new ProductAttributeCards(ProductTable);
            Attributes = productAttributeCards.productAttributes.ToList();
        }
    }
}
