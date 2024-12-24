using System.Text;
using FluentAssertions;
using TagsCloudVisualization.FileReaders;

namespace TagsCloudVisualizationTests
{
    public class TxtFileReaderTests
    {
        [Test]
        public void ReadLines_ReturnCorrect_WhenReadWordsFromFile()
        {
            var reader = new TxtFileReader("./../../../TestData/text.txt", Encoding.UTF8);

            var result = reader.ReadLines();

            result.Should().BeEquivalentTo("Всем", "Привет", "Этот", "файл", "должен", "обрабатываться", "корректно");
        }

        [Test]
        public void ReadLines_ShouldThrow_WhenFileDoesNotExists()
        {
            var reader = new TxtFileReader("text ttt ty", Encoding.UTF8);

            Action act = () => reader.ReadLines();

            act.Should().Throw<FileNotFoundException>();
        }
    }
}