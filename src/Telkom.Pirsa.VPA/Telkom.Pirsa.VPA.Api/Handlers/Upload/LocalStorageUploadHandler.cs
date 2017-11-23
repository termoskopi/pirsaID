using System;
using System.IO;
using System.Threading.Tasks;
using Nancy;
using Research.Web.Nancy.Application.Settings;

namespace Research.Web.Nancy.Application.Handlers.Upload
{
  public class LocalStorageUploadHandler : IFileUploadHandler
  {
    private readonly IApplicationSetting _setting;
    private readonly IRootPathProvider _rootProvider;

    public LocalStorageUploadHandler(IApplicationSetting setting, IRootPathProvider rootProvider)
    {
      _setting = setting;
      _rootProvider = rootProvider; 
    }

    #region IFileUploadHandler Members

    public async Task<FileUploadResult> HandleUpload(string fileName, Stream stream)
    {
      try
      {
        string uuid = GetFileName();
        string targetFile = GetTargetFile(uuid);

        using (FileStream destinationStream = File.Create(targetFile))
        {
          await stream.CopyToAsync(destinationStream);
        }

        return new FileUploadResult()
        {
          Identifier = uuid
        };
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    #endregion

    private string GetTargetFile(string fileName)
    {
      return Path.Combine(GetUploadDirectory(), fileName);
    }

    private string GetFileName()
    {
      return Guid.NewGuid().ToString();
    }

    private string GetUploadDirectory()
    {
      var uploadDirectory = Path.Combine(_rootProvider.GetRootPath(), _setting.UploadDirectory);

      if (!Directory.Exists(uploadDirectory))
      {
        Directory.CreateDirectory(uploadDirectory);
      }

      return uploadDirectory;
    }
  }
}
