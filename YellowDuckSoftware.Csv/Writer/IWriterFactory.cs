using System.Collections.Generic;

namespace YellowDuckSoftware.Csv.Writer
{
    public interface IWriterFactory
    {
        IWriter Create(string pathName, IEnumerable<string> columnNames, string commentBlock = null);
        IWriter CreateAsAppended(string pathName, IEnumerable<string> columnNames);
    }
}
