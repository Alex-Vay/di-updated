using System.Globalization;
using System.Text;
using FluentAssertions;
using TagsCloudVisualization.FileReaders;

namespace TagsCloudVisualizationTests
{
    public class CsvFileReaderTests
    {
        [Test]
        public void ReadLines_ShouldCorrect_WhenReadWordsFromFile()
        {
            var reader = new CsvFileReader("./../../../TestData/text.csv");

            var result = reader.ReadLines();

            result.Should().BeEquivalentTo("Всем", "Привет", "Этот", "файл", "должен", "обрабатываться", "корректно");
        }

        [Test]
        public void ReadLines_ShouldThrow_WhenFileDoesNotExists()
        {
            var reader = new TxtFileReader("text ttt ty");

            Action act = () => reader.ReadLines();

            act.Should().Throw<FileNotFoundException>();
        }
    }
}