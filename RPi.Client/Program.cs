using System;
using System.IO;
using System.Threading;

namespace RPi.Client
{
    class Program
    {
        static ManualResetEvent CompletedEvent = new ManualResetEvent(false);
        static bool flagRetry = true;
        public static void Main(string[] args)
        {
            if (WiringPi.Init.WiringPiSetup() != -1)
            {
                while (flagRetry)
                {
                    try
                    {
                        flagRetry = false;
                        Start();
                    }
                    catch (Exception ex)
                    {
                        flagRetry = true;
                    }
                }
            }
            else
            {
                Console.WriteLine("WiringPiSetup failed");
            }
            CompletedEvent.WaitOne();
            Console.ReadLine();
        }
        private static void Start()
        {
            Console.WriteLine("Starting...");

            SignalRHelper.Start();

            SignalRHelper.On<Models.Message>("message", OnMessage);



        }
        private static void OnMessage(Models.Message message)
        {
            Console.WriteLine(string.Format("GPIO_GEN {0}-{1} at {2} ---", message.GpioNo, message.Status, DateTime.Now.ToString()));
            if (message.Status)
            {
                WiringPi.SoftPwm.Create(message.GpioNo, 0, 100);
                WiringPi.SoftPwm.Write(message.GpioNo, 255);

            }
            else
            {
                WiringPi.SoftPwm.Stop(message.GpioNo);
            }
        }
    }

}
