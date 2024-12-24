using System.Drawing;
using System.Text;
using FluentAssertions;
using TagsCloudVisualization.FileReaders;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.FileReaders.Filters;
using TagsCloudVisualization.Visualizers;
using TagsCloudVisualization.CloudLayouter.PointsGenerators;
using TagsCloudVisualization.Visualizers.ImageColoring;


namespace TagsCloudVisualizationTests;

[TestFixture]
public class CloudGeneratorTest
{
    [Test]
    public void CloudGenerator_GenerateTagCloud_ShouldGenerateFile()
    {
        var generator = InitGenerator();

        var savePath = generator.GenerateTagCloud();

        File.Exists(savePath).Should().BeTrue();
    }

    private static CloudGenerator InitGenerator()
    {
        var fileReader = new TxtFileReader("./../../../TestData/text.txt", Encoding.UTF8);
        var imageSaver = new ImageSaver("test", "png");
        var layouter = new CircularCloudLayouter(new SpiralPointsGenerator(new Point(1000, 1000), 0.1, 0.1));
        var imageCreator = new ImageCreator(
            new Size(2000, 2000), new FontFamily("Calibri"),
            new BlackColoring(), new RandomColoring(), layouter);
        List<IFilter> filters = [new LowercaseFilter(), new BoringWordsFilter()];

        return new CloudGenerator(imageSaver, fileReader, imageCreator, filters);
    }
}