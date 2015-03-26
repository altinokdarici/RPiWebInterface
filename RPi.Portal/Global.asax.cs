using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RPi.Portal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            SaveUser();
        }
        void SaveUser()
        {
            string url = HttpContext.Current.Server.MapPath("~/App_Data/Users.json");
            List<DataAccess.User> users = new List<DataAccess.User>() {
                new DataAccess.User()
                {
                    Id=Guid.NewGuid(),
                    EMail = "altinok.darici@outlook.com",
                    Password = "123456Aa.",
                    Devices= new List<DataAccess.Device>(){ 
                            new DataAccess.Device()
                            {
                                Code="1",
                                Id=Guid.NewGuid()
                            }
                        }
                }
            };
            if (string.IsNullOrEmpty(System.IO.File.ReadAllText(url)))
            {
                System.IO.File.WriteAllText(url, Newtonsoft.Json.JsonConvert.SerializeObject(users));
            }
        }
    }
}
