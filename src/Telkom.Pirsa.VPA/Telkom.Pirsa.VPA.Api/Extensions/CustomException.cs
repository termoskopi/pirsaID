using System;
using Newtonsoft.Json.Linq;

namespace Research.Web.Nancy.Application.Extensions
{
  public class CustomException : Exception
  {
    public CustomException(Exception ex) : base(ex.Message, ex.InnerException)
    {
      
    }

    public override string ToString()
    {
      return new JObject()
      {
        new JProperty("Message", Message),
        new JProperty("StackTrace", StackTrace)
      }
      .ToString();
    }
  }
}
