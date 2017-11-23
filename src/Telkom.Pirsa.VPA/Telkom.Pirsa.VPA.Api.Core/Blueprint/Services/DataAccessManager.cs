using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Research.Web.Nancy.Application.Data.BusinessLogic;
using Research.Web.Nancy.Application.Data.Core;
using Research.Web.Nancy.Application.Data.DataAccess;

namespace Research.Web.Nancy.Application.Core.Blueprint.Services
{
 public class DataAccessManager
  {
    private readonly IConnectionManager _connection;
    private readonly DatabaseSeeder _seeder;
    private readonly DataAccessBuilder _builder;

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
      get { return _connection; }
    }

  }
}
