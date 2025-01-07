using TagsCloudVisualization.FileReaders;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization;

public class FileReadersFactory
{
    private readonly CsvFileReaderSettings csvSettings;
    private readonly TxtFileReaderSettings txtSettings;
    private readonly WordFileReaderSettings wordSettings;
    private readonly Options settings;

    public FileReadersFactory(
        CsvFileReaderSettings csvSettings, 
        TxtFileReaderSettings txtSettings, 
        WordFileReaderSettings wordSettings,
        Options settings)
    {
        this.csvSettings = csvSettings;
        this.txtSettings = txtSettings;
        this.wordSettings = wordSettings;
        this.settings = settings;
    }

    public IFileReader CreateReader()
    {
        var path = settings.FilePath;
        var extension = Path.GetExtension(path).ToLower();

        return extension switch
        {
            ".txt" => new TxtFileReader(txtSettings),
            ".csv" => new CsvFileReader(csvSettings),
            ".docx" => new WordFileReader(wordSettings),
            _ => throw new InvalidOperationException($"Unsupported file extension: {extension}")
        };
    }
}
