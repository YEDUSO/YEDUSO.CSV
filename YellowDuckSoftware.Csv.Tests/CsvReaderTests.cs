using Xunit;

namespace YellowDuckSoftware.Csv.Tests
{
    public class CsvReaderTests : ReaderTestBase
    {
        [Fact]
        public void ShouldReadWithNewlineAtEnd()
        {
            var dataFile = "CsvReaderTests1.csv";
            ReadTestStructure(dataFile);
        }

        [Fact]
        public void ShouldReadWithoutNewlineAtEnd()
        {
            var dataFile = "CsvReaderTests2.csv";
            ReadTestStructure(dataFile);
        }
    }
}
