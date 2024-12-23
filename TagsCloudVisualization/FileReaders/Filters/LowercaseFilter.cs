
namespace TagsCloudVisualization.FileReaders.Filters;

public class LowercaseFilter : IFilter
{
    public List<string> FilterText(List<string> text) =>
        text.Select(word => word.ToLower()).ToList();
   
}
