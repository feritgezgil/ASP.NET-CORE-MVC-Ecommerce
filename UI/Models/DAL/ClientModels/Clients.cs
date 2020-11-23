using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models.DAL
{
    public class Clients : BaseModel
    {
        public string desc { get; set; }
        public string image_url { get; set; }
        public bool is_corparate { get; set; }
        public string tcno { get; set; }
        public string uname { get; set; }
        public string upass { get; set; }
        public string activation_code { get; set; }
        public string email { get; set; }
        public virtual ICollection<Emails> Emails { get; set; }
        public virtual ICollection<Phones> Phones { get; set; }
        public virtual ICollection<Addresses> Addresses { get; set; }
        public virtual ICollection<Carts> Carts { get; set; }
    }
}
