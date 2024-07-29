using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using YellowDuckSoftware.Csv.Reader;

namespace YellowDuckSoftware.Csv.Tests
{
    [TestClass]
    public class PdReaderTests
    {
        [TestMethod]
        public void ShouldReadWithNewlineAtEnd()
        {
            var dataFile = "PdReaderTests1.pd";
            ReadTestStructure(dataFile);
        }

        [TestMethod]
        public void ShouldReadWithoutNewlineAtEnd()
        {
            var dataFile = "PdReaderTests1.pd";
            ReadTestStructure(dataFile);
        }

        private void ReadTestStructure(string dataFile)
        {
            using (var reader = new ReaderFactory().Create(dataFile))
            {
                var lineNum = 0;
                while (reader.Read())
                {
                    lineNum++;
                    if (lineNum == 1)
                    {
                        Assert.IsTrue(reader.ColumnNames.ToList().Contains("ID"));
                        Assert.IsTrue(reader.ColumnNames.ToList().Contains("NAME"));
                        Assert.IsTrue(reader.ColumnNames.ToList().Contains("VALUE"));
                    }

                    switch (lineNum)
                    {
                        case 1:
                            ConfirmValues(reader, "1", "ONE | ONE", "ONE1");
                            break;

                        case 2:
                            ConfirmValues(reader, "2", "TWO", "TWO2");
                            break;

                        case 3:
                            ConfirmValues(reader, "3", "THREE", "THREE3");
                            break;

                        case 4:
                            ConfirmValues(reader, "4", "FOUR", "FOUR4");
                            break;
                    }
                }
            }
        }

        private void ConfirmValues(IReader reader, string id, string name, string value)
        {
            Assert.IsTrue(reader["ID"] == id);
            Assert.IsTrue(reader["NAME"] == name);
            Assert.IsTrue(reader["VALUE"] == value);
        }
    }
}
