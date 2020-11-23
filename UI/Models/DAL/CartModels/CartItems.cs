using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models.DAL
{
    public class CartItems : BaseModel
    {
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Products Products { get; set; }

        public int ProductQuantity { get; set; }

        public int CartId { get; set; }

        [ForeignKey("CartId")]
        public Carts Carts { get; set; }
    }
}
