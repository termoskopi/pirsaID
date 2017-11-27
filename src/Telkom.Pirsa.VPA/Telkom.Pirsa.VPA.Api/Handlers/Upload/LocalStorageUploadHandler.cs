using System;
using System.IO;
using System.Threading.Tasks;
using Nancy;
using Telkom.Pirsa.VPA.Api.Settings;

namespace Telkom.Pirsa.VPA.Api.Handlers.Upload
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

    public async Task<FileUploadResult> HandleUpload(string fileName, Stream stream, string name = null)
    {
      try
      {
        string uuid = GetFileName(fileName, name);
        string targetFile = GetTargetFile(uuid);

        using (FileStream destinationStream = File.Create(targetFile))
        {
          await stream.CopyToAsync(destinationStream);
        }


        return new FileUploadResult()
        {
          Identifier = GetUploadDirectory() + "\\" + uuid
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

    private string GetFileName(string filename, string name = null)
    {
      var ext = Path.GetExtension(filename);
      if (string.IsNullOrEmpty(name))
      {
        name = Path.GetFileNameWithoutExtension(filename);
      }
      return string.Format("{0}-{1}{2}", name, Guid.NewGuid().ToString(), ext);
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
