using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Telkom.Pirsa.VPA.Api.Core.Blueprint.Services
{
  public class TokenProvider
  {
    public static string GenerateToken(string username)
    {
      try
      {
        var tick = DateTime.Now.Ticks;
        StringBuilder Sb = new StringBuilder();

        using (SHA256 hash = SHA256Managed.Create())
        {
          Encoding enc = Encoding.UTF8;
          Byte[] result = hash.ComputeHash(enc.GetBytes(string.Format("{0}_{1}", username, tick)));

          foreach (Byte b in result)
            Sb.Append(b.ToString("x2"));
        }

        return Sb.ToString();
      }
      catch (Exception ex)
      {
        throw ex;
      }
     
    }

    public static string HashPassword(string plain)
    {
      try
      {
        var tick = DateTime.Now.Ticks;
        StringBuilder Sb = new StringBuilder();

        using (SHA256 hash = SHA256Managed.Create())
        {
          Encoding enc = Encoding.UTF8;
          Byte[] result = hash.ComputeHash(enc.GetBytes(plain));

          foreach (Byte b in result)
            Sb.Append(b.ToString("x2"));
        }

        return Sb.ToString();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
