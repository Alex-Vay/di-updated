using FluentAssertions;
using System.Text;
using TagsCloudVisualization.FileReaders;
using TagsCloudVisualization.FileReaders.Processors;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class BoringWordsFilterTests
{
    private string path = "./../../../TestData/text.txt";

    [Test]
    public void BoringWordFilter_FilterText_ShouldExcludeAllBoringWords()
    {
        var filter = new BoringWordsFilter();
        var reader = new TxtFileReader(path);

        var text = reader.ReadLines();
        var filtered = filter.ProcessText(text);

        filtered.Should().BeEquivalentTo("Привет", "файл", "должен", "обрабатываться", "корректно");
    }

    [Test]
    public void BoringWordFilter_AddBoringPartOfSpeech_ShouldExcludeAllBoringWords()
    {
        var filter = new BoringWordsFilter();
        var reader = new TxtFileReader(path);

        var text = reader.ReadLines();
        filter.AddBoringPartOfSpeech("S");
        var filtered = filter.ProcessText(text);

        filtered.Should().BeEquivalentTo("должен", "обрабатываться", "корректно");
    }

    [Test]
    public void BoringWordFilter_AddBoringPartOfSpeech_ShouldExcludeBoringWord()
    {
        var filter = new BoringWordsFilter();
        var reader = new TxtFileReader(path);

        var text = reader.ReadLines();
        filter.AddBoringPartOfSpeech("должен");
        var filtered = filter.ProcessText(text);

        filtered.Should().BeEquivalentTo("Привет", "файл", "обрабатываться", "корректно");
    }
}