using System.IO;

namespace YellowDuckSoftware.Csv.Reader
{
    public class ReaderFactory : IReaderFactory
    {
        public static IReader OverriddenValueForFactory;

        public IReader Create(string pathName)
        {
            if (OverriddenValueForFactory != null)
            {
                return OverriddenValueForFactory;
            }

            var extension = Path.GetExtension(pathName)?.ToUpper();
            switch (extension)
            {
                case ".CSV":
                    return new DelimitedReader(pathName, ",");

                case ".PD":
                    return new DelimitedReader(pathName, "|");
            }
            return null;
        }
    }
}
