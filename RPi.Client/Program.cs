using System;
using System.Threading;

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
            Console.WriteLine(string.Format("GPIO_GEN {0}-{1} at {2}", message.GpioNo, message.Status, DateTime.Now.ToString()));
        }
    }

}
