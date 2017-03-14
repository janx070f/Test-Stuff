using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test_Stuff.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public string LinkName { get; set; }
        public string MenuUrl { get; set; }
        public int SortOrder { get; set; }
    }
}