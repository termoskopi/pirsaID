using System;
using Nancy;
using Nancy.Extensions;
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
      Configure();
    }

    #region IModule Members

    public void Configure()
    {
      Get["/"] = _ => GetUsers();
      Post["/"] = _ => AddUser();
      Get["/{id:int}"] = parameter => FindUser(parameter.id);
      Post["/{id:int}"] = parameter => UpdateUser(parameter.id);
      Post["/Auth"] = _ => FindUser();
      Get["/Validate"] = _ => ValidateUser();
    }
    #endregion
    public Response GetUsers()
    {
      try
      {
        var responseText = _serviceBuilder.UserService.GetAllUsers().ToString(Newtonsoft.Json.Formatting.Indented);

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
        return Response.AsText(response.ToString(), "application/json");
      }
      catch (Exception ex)
      {
        return Response.AsJson<CustomException>(new CustomException(ex), HttpStatusCode.InternalServerError);
      }
    }

    public Response FindUser()
    {
      try
      {
        var body = Request.Body.AsString(System.Text.Encoding.ASCII);
        JObject model = JObject.Parse(body);
        JObject result = _serviceBuilder.UserService.Login(model);

        return Response.AsText(result != null ? result.ToString() : null, "application/json");
      }
      catch (Exception ex)
      {
        return Response.AsJson<CustomException>(new CustomException(ex), HttpStatusCode.InternalServerError);
      }
    }

    public Response ValidateUser()
    {
      try
      {
        var token = Request.Headers.Authorization;
        if (string.IsNullOrEmpty(token))
          return Response.AsJson<string>(new JObject(new JProperty("Message", "Token not present")).ToString(), HttpStatusCode.Unauthorized);
        var result = _serviceBuilder.UserService.ValidateToken(token);
        if(result == null)
          return Response.AsJson<string>(new JObject(new JProperty("Message", "Token not valid")).ToString(), HttpStatusCode.Unauthorized);

        return Response.AsText(new JObject(new JProperty("Message", "Validated")).ToString(), "application/json");
      }
      catch (Exception ex)
      {
        return Response.AsJson<CustomException>(new CustomException(ex), HttpStatusCode.InternalServerError);
      }
    }
  }
}
