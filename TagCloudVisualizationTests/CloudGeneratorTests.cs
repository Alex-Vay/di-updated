using System.Drawing;
using System.Text;
using FluentAssertions;
using TagsCloudVisualization.FileReaders;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.FileReaders.Processors;
using TagsCloudVisualization.Visualizers;
using TagsCloudVisualization.CloudLayouter.PointsGenerators;
using TagsCloudVisualization.Visualizers.ImageColoring;
using TagsCloudVisualization.CloudLayouter.CloudGenerators;


namespace TagsCloudVisualizationTests;

[TestFixture]
public class CloudGeneratorTest
{
    [Test]
    public void CloudGenerator_GenerateTagCloud_ShouldGenerateFile()
    {
        var generator = InitGenerator();

        var savePath = Path.Combine(Directory.GetCurrentDirectory(), "test.png");

        File.Exists(savePath).Should().BeTrue();
    }

    private static CloudGenerator InitGenerator()
    {
        var fileReader = new TxtFileReader("./../../../TestData/text.txt");
        var imageSaver = new ImageSaver("test", "png");
        var layouter = new CircularCloudLayouter(new SpiralPointsGenerator(new Point(1000, 1000), 0.1, 0.1));
        var imageCreator = new BitmapCreator(
            new Size(2000, 2000), new FontFamily("Calibri"),
            new BlackColoring(), new RandomColoring(), layouter);
        List<ITextProcessor> processors = [new LowercaseTransformer(), new BoringWordsFilter()];

        return new CloudGenerator(imageSaver, fileReader, imageCreator, processors);
    }
}