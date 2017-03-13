using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test_Stuff.Models
{
    public class Subscriber
    {
        public Subscriber()
        {
            Active = true;
            UniqueKey = Guid.NewGuid();
        }
        public int SubscriberId { get; set; }
        [Display(Name = "Dit navn")]
        [Required(ErrorMessage = "Udfyld venligst dit navn")]
        public string Name { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "udfyld med korrekt email")]
        public string Email { get; set; }
        public Guid UniqueKey { get; set; }

        public bool Active { get; set; }
        public virtual List<SubscriberNewsletter> NewsletterSubscribers { get; set; }
    }
}