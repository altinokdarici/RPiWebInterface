using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPi.Client
{
    class Program
    {
        static ManualResetEvent CompletedEvent = new ManualResetEvent(false);

        public static void Main(string[] args)
        {
            //if (WiringPi.Init.WiringPiSetup() != -1)
            // {
            SignalRHelper.Start();

            SignalRHelper.On<Models.Message>("message", OnMessage);
            CompletedEvent.WaitOne();
            /* }
             else
             {
                 Console.WriteLine("WiringPiSetup failed");
             }*/
            Console.ReadLine();

        }

        private static void OnMessage(Models.Message message)
        {
            System.Console.WriteLine(string.Format("{0}-{1}", message.GpioNo, message.Status));
        }
    }

}
