using System.Collections.Generic;

namespace UI.Models.DAL
{
    public class Taxes : BaseModel
    {
        public int value { get; set; }
        public virtual ICollection<Products> ProductList { get; set; }
    }
}
