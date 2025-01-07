using TagsCloudVisualization.FileReaders;

namespace TagsCloudVisualization;

public class FileReadersFactory(Options settings)
{
    public IFileReader CreateReader()
    {
        var path = settings.FilePath;
        var extension = Path.GetExtension(path).ToLower();

        return extension switch
        {
            ".txt" => new TxtFileReader(path),
            ".csv" => new CsvFileReader(path),
            ".docx" => new WordFileReader(path),
            _ => throw new InvalidOperationException($"Unsupported file extension: {extension}")
        };
    }
}
