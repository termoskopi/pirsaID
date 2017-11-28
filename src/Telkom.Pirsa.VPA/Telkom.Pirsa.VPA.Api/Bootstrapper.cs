using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Telkom.Pirsa.VPA.Api.Authentication;
using Telkom.Pirsa.VPA.Api.Core.Blueprint;
using Telkom.Pirsa.VPA.Api.Core.Blueprint.Services;
using Telkom.Pirsa.VPA.Api.Handlers.Upload;
using Telkom.Pirsa.VPA.Api.Settings;

namespace Telkom.Pirsa.VPA.Api
{
  public class Bootstrapper : DefaultNancyBootstrapper
  {
    protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
    {
      var configuration =
    new StatelessAuthenticationConfiguration(ctx =>
    {
      if (!string.IsNullOrEmpty(ctx.Request.Headers.Authorization))
      {
        var appBuilder = container.Resolve<IApplicationServiceBuilder>();
        UserManager manager = new UserManager(appBuilder);
        var user = manager.ValidateUser(ctx.Request.Headers.Authorization);
        //appBuilder.LoggerService.LogRequestActivity(user.UserName, ctx.Request.Url.ToString());
        return user;
      }
      return null;
    });
      StatelessAuthentication.Enable(pipelines, configuration);
      base.ApplicationStartup(container, pipelines);
    }

    protected override void ConfigureApplicationContainer(TinyIoCContainer container)
    {
      container.Register<IApplicationServiceBuilder, ApplicationServiceBuilder>();
      container.Register<IFileUploadHandler, LocalStorageUploadHandler>();
      container.Register<IApplicationSetting, ApplicationSetting>();
      base.ConfigureApplicationContainer(container);


    }
  }
}
