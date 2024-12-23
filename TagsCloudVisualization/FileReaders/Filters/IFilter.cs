namespace TagsCloudVisualization.FileReaders.Filters;

public interface IFilter
{
    public List<string> FilterText(List<string> text);
}
