using System;
using System.Collections.Generic;
using Nancy;

namespace Research.Web.Nancy.Application.Handlers.Upload
{
  public class FileUploadRequest
  {
    public string Name { get; set; }

    public string Filename { get; set; }

    public HttpFile File { get; set; }
  }
}
