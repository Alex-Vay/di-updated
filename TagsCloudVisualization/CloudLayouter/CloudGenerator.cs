using TagsCloudVisualization.FileReaders;
using TagsCloudVisualization.FileReaders.Filters;
using TagsCloudVisualization.Visualizers;

namespace TagsCloudVisualization.CloudLayouter;

public class CloudGenerator(
    ImageSaver saver,
    IFileReader reader,
    ImageCreator imageCreator,
    IEnumerable<IFilter> filters)
{
    private int MIN_FONTSIZE = 20;
    private int MAX_FONTSIZE = 100;

    public string GenerateTagCloud()
    {
        var text = reader.ReadLines();
        foreach (var filter in filters)
            text = filter.FilterText(text);

        var frequencyDict = text
            .GroupBy(word => word)
            .OrderByDescending(group => group.Count())
            .ToDictionary(group => group.Key, group => group.Count());

        var maxFrequency = frequencyDict.Values.Max();
        var tagsList = frequencyDict.Select(pair => ToWordSize(pair, maxFrequency)).ToList();

        return saver.SaveImage(imageCreator.CreateBitmap(tagsList));
    }

    private WordSize ToWordSize(KeyValuePair<string, int> pair, int maxFreq)
    {
        var fontSize = (int)(MIN_FONTSIZE + (float)pair.Value / maxFreq * (MAX_FONTSIZE - MIN_FONTSIZE));
        return new(pair.Key, fontSize);
    }
        
}