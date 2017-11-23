using Research.Web.Nancy.Application.Core.Blueprint.Services;

namespace Research.Web.Nancy.Application.Core.Blueprint
{
  public interface IApplicationServiceBuilder
  {
    DatabaseSeedService SeederService { get; }
    UserService UserService { get; }

  }
}
