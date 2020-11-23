using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models.DAL
{
    public class Phones : BaseModel
    {
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual Clients Clients { get; set; }
        public string phonenumber { get; set; }
    }
}
