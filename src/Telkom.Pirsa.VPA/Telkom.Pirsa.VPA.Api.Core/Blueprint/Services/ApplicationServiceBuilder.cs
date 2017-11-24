using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telkom.Pirsa.VPA.Api.Core.Blueprint.Services
{
  public class ApplicationServiceBuilder : IApplicationServiceBuilder
  {
    private readonly DataAccessManager _dataAccess;
    private readonly DatabaseSeedService _seederService;
    private readonly UserService _userService;

    public ApplicationServiceBuilder()
    {
      _dataAccess = new DataAccessManager();
      _seederService = new DatabaseSeedService(_dataAccess);
      _userService = new UserService(_dataAccess);
    }

    public DatabaseSeedService SeederService
    {
      get { return _seederService; }
    }
    public UserService UserService
    {
        get { return _userService; }
    }


  }
}
