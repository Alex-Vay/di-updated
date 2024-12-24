using FluentAssertions;
using System.Text;
using TagsCloudVisualization.FileReaders;
using TagsCloudVisualization.FileReaders.Filters;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class BoringWordsFilterTests
{
    [Test]
    public void BoringWordFilter_FilterText_ShouldExcludeAllBoringWords()
    {
        var filter = new BoringWordsFilter();
        var reader = new TxtFileReader("TestData/text.txt", Encoding.UTF8);

        var text = reader.ReadLines();
        var filtered = filter.FilterText(text);

        filtered.Should().BeEquivalentTo("Привет", "файл", "должен", "обрабатываться", "корректно");
    }

    [Test]
    public void BoringWordFilter_AddBoringPartOfSpeech_ShouldExcludeAllBoringWords()
    {
        var filter = new BoringWordsFilter();
        var reader = new TxtFileReader("TestData/text.txt", Encoding.UTF8);

        var text = reader.ReadLines();
        filter.AddBoringPartOfSpeech("S");
        var filtered = filter.FilterText(text);

        filtered.Should().BeEquivalentTo("должен", "обрабатываться", "корректно");
    }

    [Test]
    public void BoringWordFilter_AddBoringPartOfSpeech_ShouldExcludeBoringWord()
    {
        var filter = new BoringWordsFilter();
        var reader = new TxtFileReader("TestData/text.txt", Encoding.UTF8);

        var text = reader.ReadLines();
        filter.AddBoringPartOfSpeech("должен");
        var filtered = filter.FilterText(text);

        filtered.Should().BeEquivalentTo("Привет", "файл", "обрабатываться", "корректно");
    }
}