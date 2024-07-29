using System.Collections.Generic;
using System.IO;

namespace YellowDuckSoftware.Csv.Writer
{
    public class WriterFactory : IWriterFactory
    {
        public static IWriter OverriddenValueForFactory;

        public IWriter Create(string pathName, IEnumerable<string> columnNames, string commentBlock = null)
        {
            if (OverriddenValueForFactory != null)
            {
                return OverriddenValueForFactory;
            }

            var extension = Path.GetExtension(pathName)?.ToUpper();
            switch (extension)
            {
                case ".CSV":
                    return new DelimitedWriter(pathName, columnNames, ",", false, commentBlock);

                case ".PD":
                    return new DelimitedWriter(pathName, columnNames, "|", false, commentBlock);
            }
            return null;
        }

        public IWriter CreateAsAppended(string pathName, IEnumerable<string> columnNames)
        {
            if (OverriddenValueForFactory != null)
            {
                return OverriddenValueForFactory;
            }

            var extension = Path.GetExtension(pathName)?.ToUpper();
            switch (extension)
            {
                case ".CSV":
                    return new DelimitedWriter(pathName, columnNames, ",", true, null);

                case ".PD":
                    return new DelimitedWriter(pathName, columnNames, "|", true, null);
            }
            return null;
        }
    }
}
