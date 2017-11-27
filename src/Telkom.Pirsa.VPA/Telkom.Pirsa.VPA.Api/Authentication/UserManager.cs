using System;
using System.Collections.Generic;
using System.Linq;
using Nancy.Security;
using Newtonsoft.Json.Linq;
using Telkom.Pirsa.VPA.Api.Core.Blueprint;

namespace Telkom.Pirsa.VPA.Api.Authentication
{
  public class UserManager
  {
    private readonly IApplicationServiceBuilder _serviceBuilder;

    public UserManager(IApplicationServiceBuilder serviceBuilder)
    {
      _serviceBuilder = serviceBuilder;
    }

    public IUserIdentity ValidateUser(string token)
    {
      try
      {
        var result = _serviceBuilder.UserService.ValidateToken(token);
        if (result != null)
        {
          return new UserIdentity(result["Username"].ToString(), BuildClaims(result));
        }
        return null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private IList<string> BuildClaims(JObject user)
    { 
      IList<string> claims = new List<string>();
      claims.Add(user["Username"].ToString());
      if (user["Username"].ToString().Trim() == "admin")
          claims.Add("Admin");
      else
          claims.Add("User");

      return claims;

    }
  }
}
