using System;
using System.Collections.Generic;

namespace YellowDuckSoftware.Csv.Reader
{
    public interface IReader : IDisposable
    {
        string DataSource { get; }
        IEnumerable<string> ColumnNames { get; }
        string this[string columnName] { get; }
        string this[int columnIndex] { get; }
        bool Read();
        void ReadAll(Action<IReader> row);
    }
}
