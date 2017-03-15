using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test_Stuff.Models
{
    public class BlogComment
    {
        [Key]
        public int Id { get; set; }
        public int BlogPostId { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        //public string IpAddress { get; set; }
    }
}