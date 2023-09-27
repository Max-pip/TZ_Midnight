public interface IDataProvider
{
    void Save();

    bool TryLoad();

    void Delete();
}
