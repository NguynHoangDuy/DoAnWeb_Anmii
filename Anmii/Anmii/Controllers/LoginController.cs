using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anmii.Models;

namespace Anmii.Controllers
{
    public class LoginController : Controller
    {
        private QLAnmiiEntities db = new QLAnmiiEntities();
        public ActionResult Index()
        {
            return View();
        }
        //create a string MD5

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {


            var userSession = new UserLogin();
            var result = userSession.Login(Email, Password);

            if (result == 1)
            {
                var user = userSession.GetById(Email);

                Session["FullName"] = user.HOTENNV;
                Session["imgName"] = user.ANHNV;
                return RedirectToAction("Index", "Login");
            }
            else if (result == 0)
            {
                ModelState.AddModelError("", "Tài khoản không tồn tại.");
            }
            else if (result == -2)
            {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng.");
            }

            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
    }
}
