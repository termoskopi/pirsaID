using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Extensions;
using Nancy.Responses;
using Nancy.Security;
using Telkom.Pirsa.VPA.Api.Core.Blueprint;
using Telkom.Pirsa.VPA.Api.Extensions;
using Newtonsoft.Json.Linq;

namespace Telkom.Pirsa.VPA.Api.Modules
{
    public class SettingModule : NancyModule, IModule
    {
        private readonly IApplicationServiceBuilder _serviceBuilder;

        public SettingModule(IApplicationServiceBuilder serviceBuilder) : base ("/Setting")
        {
            _serviceBuilder = serviceBuilder;
            this.RequiresAuthentication();
            Configure();
        }

        #region IModule Members
        public void Configure()
        {
            Get["/"] = _ => GetSetting();
            Post["/"] = _ => SaveSetting();
        }
        #endregion

        public Response GetSetting()
        {
            try
            {
                var setting = _serviceBuilder.SettingManagerService.LoadSetting();
                return Response.AsText(setting.ToString(), "application/json");
            }
            catch (Exception ex)
            {
                return Response.AsJson<CustomException>(new CustomException(ex), HttpStatusCode.InternalServerError);
            }
        }

        public Response SaveSetting()
        {
            try
            {
                var body = Request.Body.AsString(System.Text.Encoding.ASCII);
                JObject model = JObject.Parse(body);
                var result = _serviceBuilder.SettingManagerService.SaveSetting(model);
                _serviceBuilder.LoggerService.LogActivity(string.Format("{0} updates system setting", Context.CurrentUser.UserName), Request.Url.ToString());
                return Response.AsText(new JObject(new JProperty("Success", result)).ToString(), "application/json");
            }
            catch (Exception ex)
            {
                return Response.AsJson<CustomException>(new CustomException(ex), HttpStatusCode.InternalServerError);
            }
        }
    }
}
