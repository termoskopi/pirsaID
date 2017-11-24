using System;
using System.Collections.Generic;
using System.Linq;
using Nancy.Security;

namespace Telkom.Pirsa.VPA.Api.Authentication
{
  public class UserIdentity : IUserIdentity
  {
    private readonly string _username;
    private readonly IList<string> _claims;

    public UserIdentity(string username, IList<string> claims)
    {
      _username = username;
      _claims = claims;
    }

    #region IUserIdentity Members

    public IEnumerable<string> Claims
    {
      get { return _claims; }
    }

    public string UserName
    {
      get { return _username; }
    }

    #endregion
  }
}
