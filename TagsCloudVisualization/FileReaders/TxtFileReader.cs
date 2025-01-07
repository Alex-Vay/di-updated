using System.Text;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.FileReaders;

public class TxtFileReader(string path) : IFileReader
{
    public TxtFileReader(TxtFileReaderSettings settings)
        : this(settings.FilePath)
    { }

    public List<string> ReadLines() =>
        File.ReadAllLines(path, Encoding.UTF8)
        .Select(line => line.Split())
        .SelectMany(mas => mas)
        .Where(line => line.Length > 0)
        .ToList();
}
