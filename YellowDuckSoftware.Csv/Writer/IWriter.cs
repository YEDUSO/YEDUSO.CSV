using System;
using System.Collections.Generic;

namespace YellowDuckSoftware.Csv.Writer
{
    public interface IWriter : IDisposable
    {
        string this[string columnName] { set; }
        void WriteLine();
        void WriteRecord(Dictionary<string, string> data);
        void Close();
        void WriteRawString(string v);
    }
}
