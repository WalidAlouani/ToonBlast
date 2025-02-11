public interface IDataSerializer<T>
{
    void Save(string path, T data);
    T Load(string path);
}