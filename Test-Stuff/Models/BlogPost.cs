using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test_Stuff.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Titel")]
        [Required]
        public string Title { get; set; }
        [Display(Name = "Dato")]

        public DateTime Date { get; set; }

        [Display(Name = "Indhold")]
        [Required]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public int AdminId { get; set; }
        public virtual List<BlogComment> Comments { get; set; }

        [Display(Name = "Forfatter")]
        //public virtual Admin Admin { get; set; }
        public bool Approved { get; set; }
        public string TipperName { get; set; }
        public PostTypes PostType { get; set; }

        public enum PostTypes
        {
            [Display(Name = "Tip")]
            Tip = 0,
            [Display(Name = "BlogPost")]
            BlogPost = 1
        }
    }
}