using System.Drawing;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.Visualizers.ImageColoring;

namespace TagsCloudVisualization.Visualizers;

public class ImageCreator(
    Size size, 
    FontFamily family, 
    IImageColoring background, 
    IImageColoring coloring, 
    ICircularCloudLayouter layouter)
{
    public ImageCreator(ImageSettings settings, ICircularCloudLayouter layouter)
        : this(settings.Size, settings.Font, settings.BackgroundColor, settings.Coloring, layouter)
    { }

    public Bitmap CreateBitmap(
        List<WordSize> text
        )
    {
        var bitmap = new Bitmap(size.Width, size.Height);
        using var graphics = Graphics.FromImage(bitmap);

        graphics.Clear(background.GetNextColor());
        foreach (var word in text)
        {
            var wordColor = new SolidBrush(coloring.GetNextColor());
            var font = new Font(family, word.FontSize);
            var wordSize = CeilSize(graphics.MeasureString(word.Word, font));
            var rectPosition = layouter.PutNextRectangle(wordSize);
            graphics.DrawRectangle(new Pen(Color.White), rectPosition);
            graphics.DrawString(word.Word, font, wordColor, rectPosition);
        }
        return bitmap;
    }

        private static Size CeilSize(SizeF size)
            => new((int)size.Width + 1, (int)size.Height + 1);
}