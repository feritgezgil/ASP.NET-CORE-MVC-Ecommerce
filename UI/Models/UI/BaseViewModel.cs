using System;
using System.ComponentModel.DataAnnotations;

namespace UI.Models.UI
{
    public class BaseViewModel
    {
        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Is Active?")]
        public bool is_active { get; set; }

        [Display(Name = "Is Deleted?")]
        public bool is_delete { get; set; }
    }
}
