
namespace TagsCloudVisualization.FileReaders;

public class TxtFileReader : IFileReader
{
    public List<string> ReadLines(string path) =>
        File.ReadAllLines(path)
        //.Select(line => line.Split(" "))
        //.SelectMany(arr => arr)
        .ToList();
}
