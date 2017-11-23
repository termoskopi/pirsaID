

namespace Research.Web.Nancy.Application.Settings
{
  public interface IApplicationSetting
  {
    string UploadDirectory { get; }
    bool IsUploadBinary { get; }
    string TrainingDirectory { get; }
    string TestingDirectory { get; }

  }
}
