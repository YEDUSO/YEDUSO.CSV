namespace YellowDuckSoftware.Csv.Reader
{
    public interface IReaderFactory
    {
        IReader Create(string pathName);
    }
}
