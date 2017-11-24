namespace Telkom.Pirsa.VPA.Setting
{
  /// <summary>
  /// Represents setting object and serve as a wrapper for setting configuration.
  /// </summary>
  public class Setting
  {
    /// <summary>
    /// Default Constructor sets default values of its properties
    /// </summary>
    public Setting()
    {
      MaximumFileSize = 10;
      MaximumDuration = 60;
      PreferredImageSize = 100;
      ModifiedDate = System.DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
      TrainingImagesPath = @"data\training";
      TestImagesPath = @"data\test";
      LogPath = @"data\log";
      PreferredImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
      RecognizerConfiguration = new RecognizerSetting()
      {
        ClassifierPath = @"haar-cascades\haarcascade_frontalface_default.xml",
        MinimumNeighbour = 10,
        MinimumWindowSize = 40,
        PrincipalComponent = 80,
        ScaleFactor = 1.05,
        UseHistogramEqualizer = false,
        Threshold = 3500
      };
    }

    /// <summary>
    /// Last time the setting modified
    /// </summary>
    public string ModifiedDate { set; get; }

    /// <summary>
    /// Maximum file size setting in MB. Default value is 10
    /// </summary>
    public int MaximumFileSize { set; get; }

    /// <summary>
    /// Maximum duration of video in second. Default value is 60
    /// </summary>
    public double MaximumDuration { set; get; }

    /// <summary>
    /// Relative or absolute directory path of training image (will be created if not exist)
    /// </summary>
    public string TrainingImagesPath { set; get; }

    /// <summary>
    /// Relative or absolute directory path of tests image (will be created if not exist)
    /// </summary>
    public string TestImagesPath { set; get; }

    /// <summary>
    /// Relative or absolute directory path of log directory (will be created if not exist). Log file will be created daily
    /// in specified directory
    /// </summary>
    public string LogPath { set; get; }

    /// <summary>
    /// Square preferred dimension of image extracted from video input in pixels. Default value is 100 
    /// </summary>
    public int PreferredImageSize { set; get; }

    /// <summary>
    /// Preferred image encoding to use, Default value is JPEG encoding. The valid types are PNG, JPEG, BMP. Otherwise will set
    /// to default type JPEG.
    /// </summary>
    public System.Drawing.Imaging.ImageFormat PreferredImageFormat { set; get; }

    /// <summary>
    /// Image extension matching with preferred image format setting
    /// </summary>
    public string ImageExtension
    {
      get
      {
        if (PreferredImageFormat == System.Drawing.Imaging.ImageFormat.Bmp)
          return "bmp";
        else if (PreferredImageFormat == System.Drawing.Imaging.ImageFormat.Png)
          return "png";
        else
          return "jpg";
      }
    }

    /// <summary>
    /// Contains prdefined setting of eigen face recognizer. Changes of these settings require rerun training phase
    /// of recognizer to takes effect.
    /// </summary>
    public RecognizerSetting RecognizerConfiguration { set; get; }

  }
}
