using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telkom.Pirsa.VPA.Api.Settings
{
  public class ApplicationSetting : IApplicationSetting
  {
    #region IApplicationSetting Members

    public string UploadDirectory
    {
      get { return @"D:\Shared\Uploads"; }
    }

    public bool IsUploadBinary
    {
      get { throw new NotImplementedException(); }
    }

    public string TrainingDirectory
    {
      get { throw new NotImplementedException(); }
    }

    public string TestingDirectory
    {
      get { throw new NotImplementedException(); }
    }

    #endregion
  }
}
