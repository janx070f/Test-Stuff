using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test_Stuff.Models
{
    public class Newsletter
    {
        public Newsletter()
        {
            Send = false;
            SendTried = false;
            CreateDate = DateTime.Now;
        }
        public int NewsletterId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Send { get; set; }
        [Display(Name = "Kontrol count")]
        public int SendCount { get; set; }
        public int SendFailed { get; set; }
        public bool SendTried { get; set; }
        public virtual List<SubscriberNewsletter> NewsletterSubscribers { get; set; }
    }
}