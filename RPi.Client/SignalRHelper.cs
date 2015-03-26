using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPi.Client
{
    class SignalRHelper
    {
        static HubConnection hubConnection;
        static IHubProxy hubProxy;
        static SignalRHelper()
        {
            hubConnection = new HubConnection("http://localhost/RPi.Portal/signalr");
            hubProxy = hubConnection.CreateHubProxy("DeviceHub");
        }

        public static void Start()
        {
            hubConnection.Start().Wait();
            Console.WriteLine("Started");


        }
        public static void On<T>(string eventName, Action<T> action)
        {
            hubProxy.On<T>(eventName, action);
        }

        //messageProxy.On<Model>("messageArrived", message =>
        //{
        //    if (message.Value == 0)
        //    {
        //        SoftPwm.Stop(message.Pin);
        //    }
        //    else
        //    {
        //        SoftPwm.Create(message.Pin, 0, 100);
        //        Thread.Sleep(500);
        //        SoftPwm.Write(message.Pin, message.Value);
        //    }


        //    Console.WriteLine(message.ToString());
        //    Thread.Sleep(1000);
        //});


    }
}
