using System;
using Nancy;
using Nancy.Extensions;
using Nancy.Security;
using Newtonsoft.Json.Linq;
using Telkom.Pirsa.VPA.Api.Core.Blueprint;
using Telkom.Pirsa.VPA.Api.Core.Blueprint.Services;
using Telkom.Pirsa.VPA.Api.Extensions;

namespace Telkom.Pirsa.VPA.Api.Modules
{
  public class UserModule : NancyModule, IModule
  {

    private IApplicationServiceBuilder _serviceBuilder;
    public UserModule(IApplicationServiceBuilder builder) : base("/Users")
    {
      _serviceBuilder = builder;
      this.RequiresAuthentication();
      this.RequiresClaims(new[] { "Admin" });
      Configure();
    }

    #region IModule Members

    public void Configure()
    {
      Get["/"] = _ => GetUsers();
      Post["/"] = _ => AddUser();
      Get["/{id:int}"] = parameter => FindUser(parameter.id);
      Post["/{id:int}"] = parameter => UpdateUser(parameter.id);
    }
    #endregion

    public Response GetUsers()
    {
      try
      {
        var responseText = _serviceBuilder.UserService.GetAllUsers().ToString(Newtonsoft.Json.Formatting.Indented);
        _serviceBuilder.LoggerService.LogActivity(string.Format("{0} lists API users", Context.CurrentUser.UserName), Request.Url.ToString());
        return Response.AsText(responseText, "application/json");
      }
      catch (Exception ex)
      {
        return Response.AsJson<CustomException>(new CustomException(ex), HttpStatusCode.InternalServerError);
      }
      
    }

    public Response AddUser()
    {
      try
      {
        var body = Request.Body.AsString(System.Text.Encoding.ASCII);
        JObject model = JObject.Parse(body);
        var status = _serviceBuilder.UserService.CreateUser(model);
        var response = new JObject() 
        { 
          new JProperty("Success", status)
        };
        _serviceBuilder.LoggerService.LogActivity(string.Format("{0} add a new API user", Context.CurrentUser.UserName), Request.Url.ToString());
        return Response.AsText(response.ToString(), "application/json");
      }
      catch (Exception ex)
      {
        return Response.AsJson<CustomException>(new CustomException(ex), HttpStatusCode.InternalServerError);
      }
    }

    public Response FindUser(object id)
    {
      try
      {
        var result = _serviceBuilder.UserService.FindUser(id);
        return Response.AsText(result.ToString(), "application/json");
      }
      catch (Exception ex)
      {
        return Response.AsJson<CustomException>(new CustomException(ex), HttpStatusCode.InternalServerError);
      }
    }

    public Response UpdateUser(object id)
    {
      try
      {
        var body = Request.Body.AsString(System.Text.Encoding.ASCII);
        JObject model = JObject.Parse(body);
        var status = _serviceBuilder.UserService.Update(model, id);
        var response = new JObject() 
        { 
          new JProperty("Success", status)
        };
        _serviceBuilder.LoggerService.LogActivity(string.Format("{0} update an API user information", Context.CurrentUser.UserName), Request.Url.ToString());
        return Response.AsText(response.ToString(), "application/json");
      }
      catch (Exception ex)
      {
        return Response.AsJson<CustomException>(new CustomException(ex), HttpStatusCode.InternalServerError);
      }
    }
  }
}
