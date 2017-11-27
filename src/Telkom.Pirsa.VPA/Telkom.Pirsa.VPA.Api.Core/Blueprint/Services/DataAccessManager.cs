using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telkom.Pirsa.VPA.Api.Data.BusinessLogic;
using Telkom.Pirsa.VPA.Api.Data.Core;
using Telkom.Pirsa.VPA.Api.Data.DataAccess;

namespace Telkom.Pirsa.VPA.Api.Core.Blueprint.Services
{
 public class DataAccessManager
  {
    private readonly IConnectionManager _connection;
    private readonly DatabaseSeeder _seeder;
    private readonly DataAccessBuilder _builder;
    private readonly object _lock = new object();

    public DataAccessManager()
    {
      _builder = new DataAccessBuilder();
      _builder.Build();
      _connection = _builder.Connection;
      _seeder = _builder.Seeder;
    }

    public void SeedDatabase()
    {
      if (_builder.IsRequireSeeding)
      {
        _seeder.SeedDatabase();
      }
    }

    public string ReadSeedScript()
    {
      _seeder.ReadScript();

      return _seeder.Sql;
    }

    public IConnectionManager ConnectionManager
    {
      get 
      {
        lock (_lock)
        {
          return _connection;
        } 
      }
    }

  }
}
