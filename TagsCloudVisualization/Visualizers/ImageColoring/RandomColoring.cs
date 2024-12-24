using System.Drawing;

namespace TagsCloudVisualization.Visualizers.ImageColoring;

public class RandomColoring : IImageColoring
{
    public Color GetNextColor()
    {
        var random = new Random();
        return Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
    }
}
