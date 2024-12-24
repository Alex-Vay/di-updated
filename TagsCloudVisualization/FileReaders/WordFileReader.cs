using DocumentFormat.OpenXml.Packaging;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.FileReaders;

public class WordFileReader(string path) : IFileReader
{
    public WordFileReader(WordFileReaderSettings settings)
        : this(settings.FilePath)
    { }

    public List<string> ReadLines()
    {
        using var document = WordprocessingDocument.Open(path, false);
        var paragraphs = document.MainDocumentPart.Document.Body;
        return paragraphs
            .Select(word => word.InnerText)
            .Where(word => word.Length > 0)
            .ToList();
    }
}