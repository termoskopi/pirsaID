using Telkom.Pirsa.VPA.Api.Core.Blueprint.Services;

namespace Telkom.Pirsa.VPA.Api.Core.Blueprint
{
  public interface IApplicationServiceBuilder
  {
    DatabaseSeedService SeederService { get; }
    UserService UserService { get; }

  }
}
