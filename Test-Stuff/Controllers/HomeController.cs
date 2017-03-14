using System;
using System.Collections.Generic;
using System.Linq;
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