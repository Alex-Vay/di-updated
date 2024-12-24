using Autofac;
using CommandLine;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.CloudLayouter.PointsGenerators;
using TagsCloudVisualization.FileReaders;
using TagsCloudVisualization.FileReaders.Filters;
using TagsCloudVisualization.Visualizers;

namespace TagsCloudVisualization;

internal class Program
{
    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(settings =>
            {
                var container = BuildContainer(settings);
                var generator = container.Resolve<CloudGenerator>();
                Console.WriteLine("File saved in " + generator.GenerateTagCloud());
            });
    }

    private static IContainer BuildContainer(Options settings)
    {
        var builder = new ContainerBuilder();

        RegisterSettings(builder, settings);
        RegisterLayouters(builder, settings);
        RegisterWordsReaders(builder, settings);
        RegisterWordsFilters(builder, settings);

        builder.RegisterType<CloudGenerator>().AsSelf();
        builder.RegisterType<ImageCreator>().AsSelf();
        builder.RegisterType<ImageSaver>().AsSelf();

        return builder.Build();
    }

    private static void RegisterSettings(ContainerBuilder builder, Options settings)
    {
        builder.RegisterInstance(SettingsFactory.BuildBitmapSettings(settings)).AsSelf();
        builder.RegisterInstance(SettingsFactory.BuildFileSaveSettings(settings)).AsSelf();
        builder.RegisterInstance(SettingsFactory.BuildCsvReaderSettings(settings)).AsSelf();
        builder.RegisterInstance(SettingsFactory.BuildWordReaderSettings(settings)).AsSelf();
        builder.RegisterInstance(SettingsFactory.BuildFileReaderSettings(settings)).AsSelf();
        builder.RegisterInstance(SettingsFactory.BuildPolarSpiralSettings(settings)).AsSelf();
        builder.Register(context => SettingsFactory.BuildPointLayouterSettings(
            context.Resolve<IPointsGenerator>())).AsSelf();
        //builder.RegisterInstance(SettingsFactory.BuildSquareSpiralSettings(settings)).AsSelf();
    }

    private static void RegisterWordsReaders(ContainerBuilder builder, Options settings)
    {
        builder
            .RegisterType<TxtFileReader>().As<IFileReader>()
            .OnlyIf(_ => Path.GetExtension(settings.FilePath) == ".txt");

        builder
            .RegisterType<CsvFileReader>().As<IFileReader>()
            .OnlyIf(_ => Path.GetExtension(settings.FilePath) == ".csv");

        builder
            .RegisterType<WordFileReader>().As<IFileReader>()
            .OnlyIf(_ => Path.GetExtension(settings.FilePath) == ".docx");
    }

    private static void RegisterWordsFilters(ContainerBuilder builder, Options settings)
    {
        builder.RegisterType<LowercaseFilter>().As<IFilter>();
        builder.RegisterType<BoringWordsFilter>().As<IFilter>();
    }

    private static void RegisterLayouters(ContainerBuilder builder, Options settings)
    {
        builder
            .RegisterType<SpiralPointsGenerator>().As<IPointsGenerator>();
            //.OnlyIf(_ => settings.UsingGenerator == PossibleGenerators.POLAR_SPIRAL);

        //builder
        //    .RegisterType<SquareArchimedesSpiral>().As<IPointGenerator>()
        //    .OnlyIf(_ => settings.UsingGenerator == PossibleGenerators.SQUARE_SPIRAL);
        builder.RegisterType<CircularCloudLayouter>().As<ICircularCloudLayouter>();
    }
}
































//using System.Drawing;
//using TagsCloudVisualization.Visualizers;
//using TagsCloudVisualization.CloudLayouter;

//namespace TagsCloudVisualization;

//public static class Program
//{
//    private const int imageWidth = 1500;
//    private const int imageHeight = 1500;

//    public static void Main()
//    {
//        var imageSize = new Size(imageWidth, imageHeight);
//        var center = new Point(imageSize.Width / 2, imageSize.Height / 2);
//        var layouter = new CircularCloudLayouter(center);
//        layouter.GenerateCloud();
//        var rectangles = layouter.GeneratedRectangles;
//        var visualizer = new ImageCreator();
//        visualizer.CreateBitmap(rectangles, imageSize);
//    }
//}