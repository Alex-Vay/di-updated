using System.Text;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.FileReaders;

public class TxtFileReader(string path, Encoding encoding) : IFileReader
{
    public TxtFileReader(TxtFileReaderSettings settings)
        : this(settings.FilePath, settings.Encoding)
    { }

    public List<string> ReadLines() =>
        File.ReadAllLines(path, encoding)
        .Select(line => line.Split())
        .SelectMany(mas => mas)
        .Where(line => line.Length > 0)
        .ToList();
}
