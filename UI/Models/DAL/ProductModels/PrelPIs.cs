using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UI.Models.DAL
{
    public class PrelPIs
    {
        [Key]
        public int id { get; set; }
        public int ProductId { get; set; }
        public int ProductImageId { get; set; }
    }
}
