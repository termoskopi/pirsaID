using System;
using System.Configuration;

namespace Telkom.Pirsa.VPA.Api.Data.DataAccess
{
  class ConnectionSetting
  {
    public ConnectionSetting()
    {
      Load();
    }

    private void Load()
    {
      try
      {
        var appSetting = ConfigurationManager.AppSettings;

        if (appSetting == null || appSetting.Count == 0)
          throw new ArgumentException("Database setting must be specified!");

        Database = appSetting["DataSource"];
        Password = appSetting["Password"];
        Version = appSetting["Version"];
        UseCompression = appSetting["UseCompression"];
        MaxPool = appSetting["MaxPool"];
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string Database { private set; get; }
    public string Password { private set; get; }
    public string UseCompression { private set; get; }
    public string Version { private set; get; }
    public string MaxPool { private set; get; }
  }
}
