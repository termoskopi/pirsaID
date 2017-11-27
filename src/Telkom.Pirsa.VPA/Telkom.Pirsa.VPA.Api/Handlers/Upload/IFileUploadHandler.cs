using System.IO;
using System.Threading.Tasks;

namespace Telkom.Pirsa.VPA.Api.Handlers.Upload
{
  public interface IFileUploadHandler
  {
    Task<FileUploadResult> HandleUpload(string fileName, Stream stream, string name = null);
  }
}
