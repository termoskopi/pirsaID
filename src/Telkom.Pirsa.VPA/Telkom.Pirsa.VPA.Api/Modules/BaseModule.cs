using System;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.Responses;
using Nancy.Responses.Negotiation;
using Nancy.Security;
using Newtonsoft.Json.Linq;
using Telkom.Pirsa.VPA.Api.Core.Blueprint;
using Telkom.Pirsa.VPA.Api.Core.Blueprint.Services;
using Telkom.Pirsa.VPA.Api.Extensions;
using Telkom.Pirsa.VPA.Api.Handlers.Upload;

namespace Telkom.Pirsa.VPA.Api.Modules
{
  public class BaseModule : NancyModule, IModule
  {
    private IApplicationServiceBuilder _serviceBuilder;
    private IFileUploadHandler _uploadHandler;
    public BaseModule(IApplicationServiceBuilder builder, IFileUploadHandler uploadHandler)
    {
      _serviceBuilder = builder;
      _uploadHandler = uploadHandler;
      //this.RequiresAuthentication();
      Configure();
    }

    #region IModule Members

    public void Configure()
    {


      Get["/"] = _ =>
        {
          this.RequiresAuthentication();
          return Response.AsText("{\"Message\": \"Hello World!\"}", "application/json");
        };
      Get["/Seed"] = _ => SeedDatabase();
      Post["/Uploads", true] = async (x, ct) => await Upload();
      Get["/Load"] = _ =>
      {
        var response = _serviceBuilder.FaceRecognizerService.GetVideoProperties(@"C:\Users\Formulatrix\Downloads\Data\Data\fatimah1.mp4");

        return Response.AsText(response.ToString(), "application/json");
      };

    }
    #endregion

    public Response SeedDatabase()
    {
      try
      {
        _serviceBuilder.SeederService.SeedDatabase();

        return Response.AsText("Database Seeded!", "application/json");
      }
      catch (Exception ex)
      {
        return Response.AsJson<CustomException>(new CustomException(ex), HttpStatusCode.InternalServerError);
      }
    }

    private System.Threading.Thread worker;
    public async Task<Negotiator> Upload()
    {
      var file = this.Request.Files.FirstOrDefault();

      if (file == null)
      {
        return Negotiate
          .WithStatusCode(HttpStatusCode.InternalServerError)
          .WithReasonPhrase("The file not found!")
          .WithContentType("application/json");
      }
      var name = Request.Form.Name;
      var uploadResult = await _uploadHandler.HandleUpload(file.Name, file.Value, name);

      var response = new FileUploadResult() { Identifier = uploadResult.Identifier };
      
      Task.Factory.StartNew(() => LongRunTask(response.Identifier, name));
      
      return Negotiate
        .WithStatusCode(HttpStatusCode.OK)
        .WithModel(response)
        .WithContentType("application/json");
    }

    private object LongRunTask(string url, string name)
    {
      var response = _serviceBuilder.FaceRecognizerService.Capture("Anonymous", url, name);

      return response;
    }
  }
}
