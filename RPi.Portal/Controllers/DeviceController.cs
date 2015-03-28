using Microsoft.AspNet.SignalR;
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
            ViewBag.DeviceCode = Helpers.SessionHelper.User.Devices.FirstOrDefault(x => x.Id == deviceId).Code;
            ViewBag.DeviceId = deviceId;
            return View();
        }

        [HttpGet]
        public ActionResult ChangeStatus(int pinNo, int status, string deviceId)
        {
            if (!Helpers.SessionHelper.User.Devices.Any(x => x.Id == Guid.Parse(deviceId)))
            {
                return new HttpStatusCodeResult(400);
            }

            //Send SignalR message
            var hub = GlobalHost.ConnectionManager.GetHubContext<Hubs.DeviceHub>();
            string clientCode = Helpers.SessionHelper.User.Devices.FirstOrDefault(x => x.Id == Guid.Parse(deviceId)).Code;
            if (Hubs.DeviceHub.ConnectionIdMappings.ContainsKey(clientCode))
            {
                string connectionId = Hubs.DeviceHub.ConnectionIdMappings[clientCode];

                RPi.Models.Message m = new RPi.Models.Message() { GpioNo = pinNo, Status = status > 0 };
                hub.Clients.Client(connectionId).message(m);
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
            return new HttpStatusCodeResult(200);
        }
    }
}