using System.Linq;
using Xunit;
using YellowDuckSoftware.Csv.Reader;

namespace YellowDuckSoftware.Csv.Tests
{
    public class ReaderTestBase
    {
        public void ReadTestStructure(string dataFile)
        {
            using (var reader = new ReaderFactory().Create(dataFile))
            {
                var lineNum = 0;
                while (reader.Read())
                {
                    lineNum++;
                    if (lineNum == 1)
                    {
                        Assert.True(reader.ColumnNames.ToList().Contains("ID"));
                        Assert.True(reader.ColumnNames.ToList().Contains("NAME"));
                        Assert.True(reader.ColumnNames.ToList().Contains("VALUE"));
                    }

                    switch (lineNum)
                    {
                        case 1:
                            ConfirmValues(reader, "1", "ONE , ONE", "ONE1");
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
            Assert.True(reader["ID"] == id);
            Assert.True(reader[0] == id);
            Assert.True(reader["NAME"] == name);
            Assert.True(reader[1] == name);
            Assert.True(reader["VALUE"] == value);
            Assert.True(reader[2] == value);
        }
    }
}
