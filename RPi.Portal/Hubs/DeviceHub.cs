using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace RPi.Portal.Hubs
{
    public class DeviceHub : Hub
    {
        public static Dictionary<string, string> ConnectionIdMappings = new Dictionary<string, string>();
        public void Register(string clientCode)
        {
            if (ConnectionIdMappings.ContainsKey(clientCode))
            {
                ConnectionIdMappings[clientCode] = Context.ConnectionId;
            }
            else
            {
                ConnectionIdMappings.Add(clientCode, Context.ConnectionId);
            }
        }

    }
}