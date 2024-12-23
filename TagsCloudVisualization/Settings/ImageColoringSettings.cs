using System.Drawing;
using TagsCloudVisualization.Visualizers.ImageColoring;

namespace TagsCloudVisualization.Settings;

public record ImageSettings(Size Size, IImageColoring Coloring);
