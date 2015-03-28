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
        static string clientId;
        static HubConnection hubConnection;
        static IHubProxy hubProxy;
        static SignalRHelper()
        {
            hubConnection = new HubConnection("http://piportal.azurewebsites.net/signalr");
            hubProxy = hubConnection.CreateHubProxy("DeviceHub");
            hubConnection.StateChanged += HubConnection_StateChanged;

            if (System.IO.File.Exists("clientId"))
            {
                clientId = System.IO.File.ReadAllText("clientId");
            }
            else
            {
                clientId = DateTime.Now.ToString("HHmmss");
                System.IO.File.WriteAllText("clientId", clientId);
            }


        }

        private static void HubConnection_StateChanged(StateChange obj)
        {


            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(string.Format("State:{0}", obj.NewState));
            Console.ResetColor();
        }

        public static void Start()
        {
            hubConnection.Start().Wait();
            hubProxy.On("whoAreYou", x =>
            {
                Console.WriteLine(string.Format("Who are you? at {0}", DateTime.Now));
                Register();
            });
            Register();
            Console.WriteLine("Client Code: " + clientId);

        }
        public static void On<T>(string eventName, Action<T> action)
        {
            hubProxy.On<T>(eventName, action);
        }
        private static void Register()
        {

            hubProxy.Invoke("Register", clientId).Wait();
        }





    }
}
