using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace YellowDuckSoftware.Csv.Writer
{
    public class DelimitedWriter : IWriter
    {
        private readonly string _pathName;
        private readonly IEnumerable<string> _columnNames;
        private readonly Dictionary<string, int> _columnNameMap = new Dictionary<string, int>();
        private readonly StreamWriter _writer;
        private readonly List<string> _outputValues;
        private readonly string _delimiter;
        private readonly bool _appending;

        public string this[string columnName]
        {
            set => _outputValues[_columnNameMap[columnName]] = value;
        }

        public DelimitedWriter(string pathName, IEnumerable<string> columnNames, string delimiter, bool append, string commentBlock)
        {
            _appending = append;
            _delimiter = delimiter;
            _pathName = pathName;
            _columnNames = columnNames;

            var index = 0;
            _outputValues = new List<string>();
            foreach (var columnName in _columnNames)
            {
                _columnNameMap[columnName] = index;
                _outputValues.Add("");
                index++;
            }
            ResetOutput();

            _writer = new StreamWriter(pathName, append);

            // Appending to a file doesn't need to identify the column names.
            if (!append)
            {
                if (!string.IsNullOrEmpty(commentBlock))
                {
                    _writer.WriteLine(commentBlock.TrimEnd());
                }

                _writer.WriteLine(string.Join(_delimiter, GetQuotedOutput(_columnNames)));
            }
        }

        private void ResetOutput()
        {
            foreach (var columnName in _columnNames)
            {
                _outputValues[_columnNameMap[columnName]] = "";
            }
        }

        public void WriteRecord(Dictionary<string, string> data)
        {
            ResetOutput();
            foreach (var columnNameMap in _columnNameMap)
            {
                if (data.ContainsKey(columnNameMap.Key))
                {
                    var output = data[columnNameMap.Key];
                    _outputValues[columnNameMap.Value] = output;
                }
            }
            WriteLine();
        }

        public void WriteRawString(string value)
        {
            _writer.WriteLine(value);
        }

        public void WriteLine()
        {
            var outLine = string.Join(_delimiter, GetQuotedOutput(_outputValues));
            _writer.WriteLine(outLine);
            ResetOutput();
        }

        private IEnumerable<string> GetQuotedOutput(IEnumerable<string> values)
        {
            return values.Select(_ => (_ ?? "").Contains(_delimiter) ? $"\"{_}\"" : _);
        }

        public void Close()
        {
            if (_writer != null)
            {
                _writer.Close();
            }
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Close();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DelimitedWriter()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
