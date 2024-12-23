namespace TagsCloudVisualization.FileReaders;

public interface IFileReader
{
    public List<string> ReadLines(string path);
}
