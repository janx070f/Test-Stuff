using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_Stuff.Models;
using Helper = Test_Stuff.Extentions.Helper;

namespace Test_Stuff.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsletterController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
       
        public ActionResult EmailForm()
        {
            return View("EmailForm", new Subscriber());
        }

        [HttpPost]
        

        public ActionResult EmailForm(Subscriber sub)
        {
            var checkSub =_context.Subscribers.FirstOrDefault(x => x.Email == sub.Email);
            if (checkSub != null)
            {
                checkSub.Active = true;
                _context.SaveChanges();
            }
            else
            {

                if (ModelState.IsValid)
                {
                    _context.Subscribers.Add(sub);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
    
        public ActionResult GetNewsletters()
        {
            ViewBag.SubscriberCount = _context.Subscribers.Where(x => x.Active == true).Count().ToString();
            return View(_context.Newsletters);
        }

        [HttpGet]
       
        public ActionResult CreateNewsletter()
        {
            return View(new Newsletter());
        }

        [HttpPost]

        public ActionResult CreateNewsletter(Newsletter news)
        {
            if (ModelState.IsValid)
            {
                _context.Newsletters.Add(news);
                _context.SaveChanges();
            }
            return RedirectToAction("GetNewsletters");
        }

        [HttpGet]
        
        public ActionResult SendNewsletter(int? id)
        {
            if (id != null)
            {
                var news = _context.Newsletters.Find(id);
                if (news != null)
                {
                    return View(news);
                }
            }
            return RedirectToAction("GetNewsletters");
        }

        [HttpPost]
        
        public ActionResult SendNewsletter(Newsletter news)
        {
            if (news.Send == true && news.SendFailed == 0)
            {
                return RedirectToAction("GetNewsletters");
            }
            if (ModelState.IsValid)
            {
                var subs = _context.Subscribers.Where(x => x.Active == true).ToList();
                // foreach over subscribers
                int index = 0;
                foreach (var sub in subs)
                {
                    string mail = news.Body;
                    try
                    {
                        mail = mail.Insert(0, "<!DOCTYPE html><html><head><meta charset='utf-8' />" +
                                              "<meta name='viewport' content='width=device-width, maximum-scale=1, minimum-scale=1, user-scalable=no'>" +
                                              "<style></style></head><body>");
                        mail = mail.Replace("##NAME##", sub.Name)
                            .Replace("##EMAIL##", sub.Email)
                            .Replace("##UNSUBSCRIBELINK##",
                                "<a href='http://localhost:59566/unsubscribe/" + sub.UniqueKey +
                                "'>Unsubscribe our newsletter</a>");
                        mail = mail.Insert(mail.Length, "</body></html>");
                        var newssub = new SubscriberNewsletter
                        {
                            NewsletterId = news.NewsletterId,
                            SubscriberId = sub.SubscriberId,
                            Tracking = EmailTracking.Started,
                            UpdateTime = DateTime.Now
                        };
                        var relation =
                            _context.NewsletterSubscribers.Any(
                                x => x.NewsletterId == newssub.NewsletterId && x.SubscriberId == newssub.SubscriberId);
                        if (!relation)
                        {
                            if (Helper.Mail.MailSender(sub.Email, news.Title, mail))
                            {
                                _context.NewsletterSubscribers.Add(newssub);
                                _context.SaveChanges();
                                if (newssub.Tracking == EmailTracking.Failed)
                                {
                                    newssub.Tracking = EmailTracking.Resend;
                                }
                                else
                                {
                                    newssub.Tracking = EmailTracking.Send;
                                }
                                news.SendCount = ++index;
                                newssub.UpdateTime = DateTime.Now;
                                news.Send = true;
                                news.SendTried = true;

                            }
                            else
                            {
                                news.SendTried = true;
                                news.SendFailed += 1;
                                newssub.UpdateTime = DateTime.Now;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex);
                    }
                    _context.Entry(news).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("GetNewsletters");
        }

        [HttpGet]

        public ActionResult Unsubscribe(string s)
        {
            var sub = _context.Subscribers.FirstOrDefault(x => x.UniqueKey == new Guid(s));
            if (sub != null)
            {
                sub.Active = false;
                _context.Entry(sub).State = EntityState.Modified;
                _context.SaveChanges();
                return View(sub);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetSubs()
        {
            return View(_context.Subscribers);
        }

        public ActionResult Delete(int id)
        {
            var sub = _context.Subscribers.Find(id);

            _context.Subscribers.Remove(sub);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult AddSubscriber(string NewsName, string NewsEmail)
        {

            if (NewsName == "" && NewsEmail == "")
            {
                return RedirectToAction("Index", "Home", new {added = "error"});
            }

            Subscriber sub = new Subscriber();
            sub.Email = NewsEmail;
            sub.Name = NewsName;
            sub.Active = true;
            _context.Subscribers.Add(sub);
           _context.SaveChanges();

            return RedirectToAction("Index", "Home", new {added = "true"});
        }

    }
}