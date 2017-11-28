using Nancy;
using Nancy.Extensions;
using Nancy.Responses.Negotiation;
using Nancy.Security;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telkom.Pirsa.VPA.Api.Core.Blueprint;
using Telkom.Pirsa.VPA.Api.Extensions;
using Telkom.Pirsa.VPA.Api.Handlers.Upload;

namespace Telkom.Pirsa.VPA.Api.Modules
{
    public class RecognizerModule : NancyModule, IModule
    {
        private IApplicationServiceBuilder _serviceBuilder;
        private IFileUploadHandler _uploadHandler;

        public RecognizerModule(IApplicationServiceBuilder builder, IFileUploadHandler handler) : base ("/Recognizer")
        {
            _serviceBuilder = builder;
            _uploadHandler = handler;
            this.RequiresAuthentication();
            Configure();
        }

        #region IModule Members
        public void Configure()
        {
            Post["Capture", true] = async (x, ct) => await Capture();
            Post["Recognize", true] = async (x, ct) => await Test();
            Get["Training", true] = async (x, ct) => await Training();
            Get["Schedules"] = _ => GetSystemTasks();
            Get["Results"] = _ => GetTestResults();
            Get["ResultImages"] = _ => GetTestedImages();
            Post["ViewImage"] = _ => GetImage();
        }
        #endregion

        public async Task<Negotiator> Capture()
        {
            try
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
                var responseText = response.Identifier + " uploaded! Capture action will queued to task scheduler access SystemTask modules to monitor tasks";
                await Task.Factory.StartNew(() => CaptureAction(response.Identifier, name));

                return Negotiate
                  .WithStatusCode(HttpStatusCode.OK)
                  .WithModel(responseText)
                  .WithContentType("application/json");
            }
            catch (Exception ex)
            {
                return Negotiate
                   .WithContentType("application/json")
                   .WithStatusCode(HttpStatusCode.InternalServerError)
                   .WithModel(new CustomException(ex));
            }
        }

        public async Task<Negotiator> Test()
        {
            try
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
                var responseText = response.Identifier + " uploaded! Test action will queued to task scheduler access SystemTask modules to monitor tasks";
                await Task.Factory.StartNew(() => TestAction(response.Identifier));

                return Negotiate
                  .WithStatusCode(HttpStatusCode.OK)
                  .WithModel(responseText)
                  .WithContentType("application/json");
            }
            catch (Exception ex)
            {
                return Negotiate
                    .WithContentType("application/json")
                    .WithStatusCode(HttpStatusCode.InternalServerError)
                    .WithModel(new CustomException(ex));
            }
        }

        public async Task<Response> Training()
        {
            try
            {
               //await Task.Factory.StartNew(() => _serviceBuilder.FaceRecognizerService.Train(Context.CurrentUser.UserName));
               Task.Factory.StartNew(() => TrainingAction());
               return Response.AsText(new JObject( new JProperty("Message","Training action will queued to task scheduler access SystemTask modules to monitor tasks")).ToString(), "application/json");
            }
            catch (Exception ex)
            {
                return Response.AsJson<CustomException>(new CustomException(ex), HttpStatusCode.InternalServerError);
            }
        }

        public Response GetSystemTasks()
        {
            try
            {
                var result = _serviceBuilder.FaceRecognizerService.GetSchedules();
                return Response.AsText(result.ToString(), "application/json");
            }
            catch (Exception ex)
            {
                return Response.AsJson<CustomException>(new CustomException(ex), HttpStatusCode.InternalServerError);
            }
        }

        public Response GetTestResults()
        {
            try
            {
                var result = _serviceBuilder.FaceRecognizerService.GetResults();
                return Response.AsText(result.ToString(), "application/json");
            }
            catch (Exception ex)
            {
                return Response.AsJson<CustomException>(new CustomException(ex), HttpStatusCode.InternalServerError);
            }
        }

        public Response GetTestedImages()
        {
            try
            {
                var result = _serviceBuilder.FaceRecognizerService.GetImageResults();
                return Response.AsText(result.ToString(), "application/json");
            }
            catch (Exception ex)
            {
                return Response.AsJson<CustomException>(new CustomException(ex), HttpStatusCode.InternalServerError);
            }
        }

        public Response GetImage()
        {
            try
            {
                var body = Request.Body.AsString(System.Text.Encoding.ASCII);
                JObject model = JObject.Parse(body);
                return Response.AsImage(model["Url"].ToString());
            }
            catch (Exception ex)
            {
                return Response.AsJson<CustomException>(new CustomException(ex), HttpStatusCode.InternalServerError);
            }
        }

        #region Background Tasks
        public object CaptureAction(string path, string name)
        {
            try
            {
                return _serviceBuilder.FaceRecognizerService.Capture(Context.CurrentUser.UserName, path, name);
            }
            catch(Exception ex)
            {
                _serviceBuilder.LoggerService.LogActivity("Exception occured: " + ex.Message, "RecognizerModule.Capture()");
                return false;
            }
        }

        public object TestAction(string path)
        {
            try
            {
                return _serviceBuilder.FaceRecognizerService.Test(Context.CurrentUser.UserName, path);
            }
            catch (Exception ex)
            {
                _serviceBuilder.LoggerService.LogActivity("Exception occured: " + ex.Message, "RecognizerModule.Test()");
                return false;
            }
        }

        public object TrainingAction()
        {
            try
            {
                return _serviceBuilder.FaceRecognizerService.Train(Context.CurrentUser.UserName);
            }
            catch (Exception ex)
            {
                _serviceBuilder.LoggerService.LogActivity("Exception occured: " + ex.Message, "RecognizerModule.Training()");
                return false;
            }
        }
        #endregion
    }
}
