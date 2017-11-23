using System.IO;
using System.Threading.Tasks;

namespace Research.Web.Nancy.Application.Handlers.Upload
{
  public interface IFileUploadHandler
  {
    Task<FileUploadResult> HandleUpload(string fileName, Stream stream);
  }
}
