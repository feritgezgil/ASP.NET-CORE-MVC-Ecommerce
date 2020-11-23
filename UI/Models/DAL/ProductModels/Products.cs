using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models.DAL
{
    public class Products : BaseModel
    {
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

        [Display(Name = "Tax")]
        public int tax_id { get; set; }

        [Display(Name = "Tax")]
        [ForeignKey("tax_id")]
        public virtual Taxes Taxes { get; set; }

        [Display(Name = "Menşei")]
        public int mensei_id { get; set; }

        [Display(Name = "Menşei")]
        [ForeignKey("mensei_id")]
        public virtual Menseis Menseis { get; set; }
    }
}
