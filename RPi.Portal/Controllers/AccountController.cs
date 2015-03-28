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
                    return RedirectToAction("Index", "Device");
                }
            }
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            Helpers.SessionHelper.User = null;
            return RedirectToAction("Login");

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.CreateAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            using (DataAccess.Entity ent = new DataAccess.Entity())
            {
                if (ent.IsUserExist(model.Email))
                {
                    ModelState.AddModelError("", "This email address is already registed!");
                    return View();
                }

                ent.SaveUser(new DataAccess.User()
                {
                    EMail = model.Email,
                    Password = model.Password,
                    Devices = new List<DataAccess.Device>(),
                    Id = Guid.NewGuid()
                });
                Helpers.SessionHelper.User = ent.GetUser(model.Email, model.Password);
                return RedirectToAction("Index", "Device");
            }
        }
    }
}