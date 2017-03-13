using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test_Stuff.ViewModels
{
    public class MailViewModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(40)]
        [Display(Name = "Afsender")]
        public string From { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(40)]
        [Display(Name = "Modtager")]
        public string Reciever { get; set; }

        [Required]
        [MaxLength(40)]
        [Display(Name = "Emne")]
        public string Subject { get; set; }

        [Required]
        [MaxLength(40)]
        [Display(Name = "Navn")]
        public string Name { get; set; }

        [Required]
        [MaxLength(2000)]
        [Display(Name = "Besked")]
        public string Message { get; set; }

        [Required]
        [MaxLength(2200)]
        [Display(Name = "HTML Body")]
        public string Body { get; set; }
    }
}