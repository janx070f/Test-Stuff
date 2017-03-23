using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Test_Stuff.Models;
using Test_Stuff.ViewModels;
using Recaptcha.Web;
using hbehr.recaptcha;
using hbehr.recaptcha.Exceptions;

namespace Test_Stuff.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _Navbar()
        {
            ViewBag.Menu = _context.Menus.ToList();
            return PartialView();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region Newsletter

        [HttpPost]
        public ActionResult TipMail(MailViewModel modelEmail)
        {
            string userResponse = HttpContext.Request.Params["g-recaptcha-response"];
            bool validCaptcha = ReCaptcha.ValidateCaptcha(userResponse);
            if (validCaptcha)
            {
                // Real User, validated !
                SendTipMail.SendEmails(string.Empty, modelEmail.Reciever, string.Empty, modelEmail.Name, modelEmail.Message);

                return View();
            }
            else
            {
                // Not validated !
                return View();
            }

        }

        #endregion

        #region Blog

        public ActionResult BlogIndex()
        {
            return View(_context.BlogPosts.ToList());
        }

        // Blog/Create
      
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Date,Content,AdminId, Title")] BlogPost blogPost)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        blogPost.PostType = BlogPost.PostTypes.BlogPost;
        //        blogPost.Approved = true;
        //        blogPost.Date = DateTime.Now;
        //        blogPost.AdminId = 2;
        //        _context.BlogPosts.Add(blogPost);
        //        _context.SaveChanges();
        //        return RedirectToAction("BlogIndex");
        //    }

        //    return View(blogPost);
        //}

        //// Blog/Edit
        
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BlogPost blogPost = _context.BlogPosts.Find(id);
        //    if (blogPost == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(blogPost);
        //}
        
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Date,Content,AdminId")] BlogPost blogPost)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Entry(blogPost).State = EntityState.Modified;
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(blogPost);
        //}

        //// Blog/Delete

        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BlogPost blogPost = _context.BlogPosts.Find(id);
        //    if (blogPost == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(blogPost);
        //}

      
        [HttpPost, ActionName("Delete")]

        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = _context.BlogPosts.Find(id);
            _context.BlogPosts.Remove(blogPost);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}