using System;
using System.Collections.Generic;
using System.Linq;
using Nancy.Security;

namespace Research.Web.Nancy.Application.Authentication
{
  public class UserIdentity : IUserIdentity
  {
    private string _username;
    private IList<string> _claims;

    public bool ValidateUserIdentity()
    {
      try
      {
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
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
