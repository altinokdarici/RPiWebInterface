using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RPi.Portal.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.LoginViewModel model)
        {
            using (DataAccess.Entity ent = new DataAccess.Entity())
            {
                var user = ent.GetUser(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Hatali email veya şifre");
                    return View();
                }
                else
                {
                    Helpers.SessionHelper.User = user;
                    return RedirectToAction("Manage", "Device");
                }
            }
        }
    }
}