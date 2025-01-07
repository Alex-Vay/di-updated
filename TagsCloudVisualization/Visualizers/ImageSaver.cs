using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Drawing;
using TagsCloudVisualization.Settings;
namespace TagsCloudVisualization.Visualizers;

public class ImageSaver(string imageName, string imageFormat, string outputpath)
{
    private readonly List<string> supportedFormats = ["png", "jpg", "jpeg", "bmp"];

    public ImageSaver(ImageSaveSettings settings)
        : this(settings.ImageName, settings.ImageFormat, settings.OutputPath)
    { }

    public string SaveImage(Bitmap image)
    {
        if (!supportedFormats.Contains(imageFormat))
            throw new ArgumentException($"Unsupported image format: {imageFormat}");

        var fullImageName = $"{imageName}.{imageFormat}";
        if (outputpath == null)
        {
            image.Save(fullImageName);
            return Path.Combine(Directory.GetCurrentDirectory(), fullImageName);
        }
        image.Save(Path.Combine(outputpath, fullImageName));
        return Path.Combine(outputpath, fullImageName);
    }
}
