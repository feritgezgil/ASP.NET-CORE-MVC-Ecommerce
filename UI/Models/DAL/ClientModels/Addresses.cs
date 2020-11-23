using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models.DAL
{
    public class Addresses : BaseModel
    {
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual Clients Clients { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string postcode { get; set; }
        public bool addresstype { get; set; }

    }
}
