using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RPi.Portal.Controllers
{
    public class DeviceController : Controller
    {
        // GET: Device
        public ActionResult Index()
        {
            return View(Helpers.SessionHelper.User.Devices);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DataAccess.Device device)
        {
            using (DataAccess.Entity ent = new DataAccess.Entity())
            {
                ent.AddDevice(device.Code, Helpers.SessionHelper.User.Id);
                Helpers.SessionHelper.User = ent.GetUser(Helpers.SessionHelper.User.Id);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Control(Guid deviceId)
        {
            return View();
        }
    }
}