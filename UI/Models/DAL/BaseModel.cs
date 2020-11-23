using System;
using System.ComponentModel.DataAnnotations;

namespace UI.Models.DAL
{
    public class BaseModel
    {
        [Display(Name ="ID")]
        [Key]
        public int id { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Create Date")]
        public DateTime c_date { get; set; }

        [Display(Name = "SEO Friendly URL")]
        public string seo_url { get; set; }

        [Display(Name = "Is Active?")]
        public bool is_active { get; set; }

        [Display(Name = "Is Deleted?")]
        public bool is_delete { get; set; }
    }
}
