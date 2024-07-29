using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowDuckSoftware.Csv.Reader;
using YellowDuckSoftware.Csv.Writer;

namespace YellowDuckSoftware.Csv.Tests
{
    [TestClass]
    public class CsvWriterTests : ReaderTestBase
    {
        [TestMethod]
        public void ShouldWriteCorrectly()
        {
            var dataFile = "CsvWriterTests1.csv";
            if (File.Exists(dataFile))
            {
                File.Delete(dataFile);
            }

            using (var writer = new WriterFactory().Create(dataFile, new List<string>() { "ID", "NAME", "VALUE" }))
            {
                writer["ID"] = "1";
                writer["NAME"] = "ONE , ONE";
                writer["VALUE"] = "ONE1";
                writer.WriteLine();

                writer["ID"] = "2";
                writer["NAME"] = "TWO";
                writer["VALUE"] = "TWO2";
                writer.WriteLine();

                writer["ID"] = "3";
                writer["NAME"] = "THREE";
                writer["VALUE"] = "THREE3";
                writer.WriteLine();

                writer["ID"] = "4";
                writer["NAME"] = "FOUR";
                writer["VALUE"] = "FOUR4";
                writer.WriteLine();
            }

            ReadTestStructure(dataFile);
        }

        [TestMethod]
        public void ShouldWriteFromDictionary()
        {
            var dataFile = "CsvWriterTests2.csv";
            if (File.Exists(dataFile))
            {
                File.Delete(dataFile);
            }

            var data = new Dictionary<string, string>();
            using (var writer = new WriterFactory().Create(dataFile, new List<string>() { "ID", "NAME", "VALUE" }))
            {
                data["ID"] = "1";
                data["NAME"] = "ONE , ONE";
                data["VALUE"] = "ONE1";
                writer.WriteRecord(data);

                data["ID"] = "2";
                data["NAME"] = "TWO";
                data["VALUE"] = "TWO2";
                writer.WriteRecord(data);

                data["ID"] = "3";
                data["NAME"] = "THREE";
                data["VALUE"] = "THREE3";
                writer.WriteRecord(data);

                data["ID"] = "4";
                data["NAME"] = "FOUR";
                data["VALUE"] = "FOUR4";
                writer.WriteRecord(data);
            }

            ReadTestStructure(dataFile);
        }
    }
}
