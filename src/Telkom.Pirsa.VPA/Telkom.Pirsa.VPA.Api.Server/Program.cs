using System;
using Nancy.Hosting.Self;

namespace Telkom.Pirsa.VPA.Api.Server
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        RunServer();
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
        var url = "http://localhost:9000";
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
       
        Console.ReadKey();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
