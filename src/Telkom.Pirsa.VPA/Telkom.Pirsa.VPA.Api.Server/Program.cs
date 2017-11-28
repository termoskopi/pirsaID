using System;
using Nancy.Hosting.Self;
using System.Configuration;
using Telkom.Pirsa.VPA.Engine.Helper;

namespace Telkom.Pirsa.VPA.Api.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RunTests();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message: {0}", ex.Message);
                Console.WriteLine("Stack Trace: {0}", ex.StackTrace);
                Console.ReadKey();
            }

        }


        private static void RunServer()
        {
            try
            {
                var setting = ConfigurationManager.AppSettings;
                var url = string.Format("{0}:{1}", setting["ServerHost"], setting["ServerPort"]);
                using (var host = new NancyHost(new Uri(url)))
                {
                    host.Start();
                    Console.WriteLine("Application Server Running on {0}", url);
                    Console.WriteLine();
                    Console.WriteLine("Press anykey to stop server.");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void RunTests()
        {
            try
            {

                var properties = VideoCapture.LoadVideo(@"E:\Users\Rohmad Raharjo\Videos\Captures\fatimah1.mp4", Setting.SettingManager.ReadFromFile());
              //Console.WriteLine(response.ToString(Newtonsoft.Json.Formatting.Indented));
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
