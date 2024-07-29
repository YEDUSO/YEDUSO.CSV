using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace YellowDuckSoftware.Csv.Reader
{
    public sealed class DelimitedReader : IReader
    {
        private string GetParsePattern()
        {
            return $"(?:^|{_regexDlimiter})(\"(?:[^\"]+|\"\")*\"|[^{_regexDlimiter}]*)";
        }

        public IEnumerable<string> ColumnNames { get; }
        public string DataSource { get; }
        private readonly string _pathName;
        private readonly Dictionary<string, int> _columnNameMap = new Dictionary<string, int>();
        private readonly StreamReader _reader;
        private IEnumerable<string> _currentLine;
        private readonly string _delimiter;
        private readonly string _regexDlimiter;

        public string this[string columnName]
        {
            get
            {
                var result = "";
                if (!_columnNameMap.ContainsKey(columnName))
                {
                    throw new KeyNotFoundException($"{DataSource} can not find key '{columnName}' in columns {string.Join(", ", _columnNameMap.Select(_ => _.Key))}");
                }
                result = _currentLine.ElementAtOrDefault(_columnNameMap[columnName]);
                return result;
            }
        }

        public string this[int columnIndex]
        {
            get
            {
                var columnName = ColumnNames.ElementAt(columnIndex);
                return this[columnName];
            }
        }

        public DelimitedReader(string pathName, string delimiter)
        {
            DataSource = pathName;
            _delimiter = delimiter;
            _regexDlimiter = _delimiter == "|"
                ? "\\|"
                : _delimiter;

            _pathName = pathName;
            _reader = new StreamReader(pathName);

            ColumnNames = ParseLine(ReadLine());
            var index = 0;
            foreach (var columnName in ColumnNames)
            {
                _columnNameMap[columnName] = index;
                index++;
            }
        }

        private string ReadLine()
        {
            string line = null;
            do
            {
                line = _reader.ReadLine();
            } while (line != null && line.Length > 0 && (line[0] == ';' || line[0] == '#'));
            return line;
        }

        public bool Read()
        {
            var line = ReadLine();
            if (line != null)
            {
                _currentLine = ParseLine(line);
            }

            return line != null;
        }

        private IEnumerable<string> ParseLine(string line)
        {
            var result = new List<string>();
            if (string.IsNullOrEmpty(line))
            {
                return result;
            }

            var parsed = System.Text.RegularExpressions.Regex.Split(line, GetParsePattern());
            var index = 0;
            foreach (var parse in parsed)
            {
                index++;
                if (index % 2 == 0)
                {
                    var val = parse;
                    if ((parse.StartsWith("\"") || parse.StartsWith("'")) &&
                        (parse.EndsWith("\"") || parse.EndsWith("'")))
                    {
                        val = parse.Substring(1, parse.Length - 2);
                    }
                    result.Add(val);
                }
            }
            return result;
        }

        public void ReadAll(Action<IReader> row)
        {
            while (Read())
            {
                row.Invoke(this);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_reader != null)
                    {
                        _reader.Dispose();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DelimitedReader()
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
