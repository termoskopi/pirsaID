using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Research.Web.Nancy.Application.Core.Blueprint.Services
{
  public class DatabaseSeedService
  {
    private readonly DataAccessManager _manager;

    public DatabaseSeedService()
    {
      throw new Exception("This default constructor has been disbaled. All Service instance only generated on ApplicationServiceBuilder");
    }

    public DatabaseSeedService(DataAccessManager manager)
    {
      _manager = manager;
    }

    public string ReadSeedScript()
    {
      return _manager.ReadSeedScript();
    }

    public void SeedDatabase()
    {
      _manager.ReadSeedScript();
      _manager.SeedDatabase();
    }
  }
}
