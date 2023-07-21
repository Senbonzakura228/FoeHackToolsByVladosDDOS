public interface IDataSaveService
{
    bool SaveData<T>(string path, T data);

    T LoadData<T>(string path);
}