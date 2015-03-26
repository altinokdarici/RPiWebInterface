using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace RPi.Portal.Hubs
{
    public class DeviceHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}