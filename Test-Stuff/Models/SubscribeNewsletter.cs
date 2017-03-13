using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test_Stuff.Models
{
  
        public class SubscriberNewsletter
        {
            [Key, Column(Order = 0)]
            public int NewsletterId { get; set; }
            [Key, Column(Order = 1)]
            public int SubscriberId { get; set; }
            public virtual Newsletter Newsletter { get; set; }
            public virtual Subscriber Subscriber { get; set; }
            public EmailTracking Tracking { get; set; }
            public DateTime UpdateTime { get; set; }
        }

    public enum EmailTracking
        {
            Started,
            Send,
            Failed,
            Resend
        }
   
}