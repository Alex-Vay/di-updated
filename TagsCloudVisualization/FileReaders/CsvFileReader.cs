using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.FileReaders;

public class CsvFileReader(string path, CultureInfo cultureInfo) : IFileReader
{
    private class TableCell
    {
        [Index(0)]
        public string Word { get; set; }
    }

    public CsvFileReader(CsvFileReaderSettings settings)
        : this(settings.FilePath, settings.Culture)
    { }

    public List<string> ReadLines()
    {
        var configuration = new CsvConfiguration(cultureInfo)
        {
            HasHeaderRecord = false
        };
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, configuration);
        return csv.GetRecords<TableCell>()
            .Select(cell => cell.Word)
            .Where(word => word.Length > 0)
            .ToList();
    }

}