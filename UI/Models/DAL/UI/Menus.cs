using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models.DAL
{
    public class Menus : BaseModel
    {
        public bool is_icon_menu { get; set; }
        public virtual ICollection<MenuItems> MenuItems { get; set; }
    }

    public class MenuItems : BaseModel
    {
        public int menuId { get; set; }
        [ForeignKey("menuId")]
        public virtual Menus Menus { get; set; }
        public string url { get; set; }
        public string icon { get; set; }
    }
}
