using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Research.Web.Nancy.Application.Core.Blueprint;
using Research.Web.Nancy.Application.Core.Blueprint.Services;
using Research.Web.Nancy.Application.Handlers.Upload;
using Research.Web.Nancy.Application.Settings;

namespace Research.Web.Nancy.Application
{
  public class Bootstrapper : DefaultNancyBootstrapper
  {
    protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
    {
      var configuration =
    new StatelessAuthenticationConfiguration(ctx =>
    {
      if (string.IsNullOrEmpty(ctx.Request.Headers.Authorization))
      {
        return null;
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
