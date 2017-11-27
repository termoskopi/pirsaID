using Nancy;
using Nancy.Extensions;
using Nancy.Responses;
using Newtonsoft.Json.Linq;
using System;
using Telkom.Pirsa.VPA.Api.Core.Blueprint;
using Telkom.Pirsa.VPA.Api.Extensions;


namespace Telkom.Pirsa.VPA.Api.Modules
{
    public class AuthenticationModule : NancyModule, IModule
    {
        private readonly IApplicationServiceBuilder _serviceBuilder;
        
        public AuthenticationModule(IApplicationServiceBuilder serviceBuilder) : base ("/Auth")
        {
            _serviceBuilder = serviceBuilder;
            Configure();
        }

        #region IModule Members
        public void Configure()
        {
            Post["/"] = _ => FindUser();
            Get["/Validate"] = _ => ValidateUser();
        }
        #endregion
        public Response FindUser()
        {
            try
            {
                var body = Request.Body.AsString(System.Text.Encoding.ASCII);
                JObject model = JObject.Parse(body);
                JObject result = _serviceBuilder.UserService.Login(model);
                if (result == null)
                    return new TextResponse(HttpStatusCode.Unauthorized, new JObject(new JProperty("Message", "The username and/or password not valid")).ToString())
                        .WithContentType("application/json");
                _serviceBuilder.LoggerService.LogActivity(string.Format("{0} do authentication request", model["Username"]), Request.Url.ToString());
                return Response.AsText(result.ToString(), "application/json");
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
                    return new TextResponse(new JObject(new JProperty("Message", "Token not present")).ToString())
                        .WithStatusCode(HttpStatusCode.Unauthorized)
                        .WithContentType("application/json");
                _serviceBuilder.LoggerService.LogActivity(string.Format("{0} request token validation", token), Request.Url.ToString());
                var result = _serviceBuilder.UserService.ValidateToken(token);
                if (result == null)
                    return new TextResponse(new JObject(new JProperty("Message", "Token not valid!")).ToString())
                        .WithStatusCode(HttpStatusCode.Unauthorized)
                        .WithContentType("application/json");

                return Response.AsText(new JObject(new JProperty("Message", "Validated")).ToString(), "application/json");
            }
            catch (Exception ex)
            {
                return Response.AsJson<CustomException>(new CustomException(ex), HttpStatusCode.InternalServerError);
            }
        }
    }
}
