﻿using System.Drawing;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Visualizers;

public class ImageSaver(string imageName, string imageFormat)
{
    private readonly List<string> supportedFormats = ["png", "jpg", "jpeg", "bmp"];

    public ImageSaver(ImageSaveSettings settings)
        : this(settings.ImageName, settings.ImageFormat)
    { }

    public string SaveImage(Bitmap image)
    {
        if (!supportedFormats.Contains(imageFormat))
            throw new ArgumentException($"Unsupported image format: {imageFormat}");

        var fullImageName = $"{imageName}.{imageFormat}";
        image.Save(fullImageName);
        return Path.Combine(Directory.GetCurrentDirectory(), fullImageName);
    }
}
