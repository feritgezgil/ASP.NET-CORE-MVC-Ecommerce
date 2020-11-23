using System.Collections.Generic;

namespace UI.Models.DAL
{
    public class Menseis : BaseModel
    {
        public string value { get; set; }
        public virtual ICollection<Products> ProductList { get; set; }
    }
}
