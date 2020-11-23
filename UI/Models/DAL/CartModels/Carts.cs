using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models.DAL
{
    public class Carts : BaseModel
    {
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Clients Clients { get; set; }

        public int status { get; set; }

        public ICollection<CartItems> CartItems { get; set; }
    }
}
