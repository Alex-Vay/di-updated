using FluentAssertions;
using System.Text;
using TagsCloudVisualization.FileReaders;
using TagsCloudVisualization.FileReaders.Filters;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class LowercaseFilterTests
{
    private readonly LowercaseFilter filter = new();

    [Test]
    public void LowercaseFilter_FilterText_ShouldLowercaseAllWords()
    {
        var reader = new TxtFileReader("TestData/text.txt", Encoding.UTF8);

        var text = reader.ReadLines();
        var filtered = filter.FilterText(text);
        
        filtered.Should().BeEquivalentTo("всем", "привет", "этот", "файл", "должен", "обрабатываться", "корректно");
    }
}