using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Test_Stuff.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<SubscriberNewsletter> NewsletterSubscribers { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }

        //public DbSet<SendTipMail> SendTipMails { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}