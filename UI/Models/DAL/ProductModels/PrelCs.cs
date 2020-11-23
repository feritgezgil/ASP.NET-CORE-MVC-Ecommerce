using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UI.Models.DAL
{
    public class PrelCs
    {
        [Key]
        public int id { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
    }
}
