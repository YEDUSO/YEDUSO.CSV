using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using YellowDuckSoftware.Csv.Reader;

namespace YellowDuckSoftware.Csv.Tests
{
    [TestClass]
    public class CsvReaderTests : ReaderTestBase
    {
        [TestMethod]
        public void ShouldReadWithNewlineAtEnd()
        {
            var dataFile = "CsvReaderTests1.csv";
            ReadTestStructure(dataFile);
        }

        [TestMethod]
        public void ShouldReadWithoutNewlineAtEnd()
        {
            var dataFile = "CsvReaderTests2.csv";
            ReadTestStructure(dataFile);
        }
    }
}
