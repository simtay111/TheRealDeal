namespace RecreateMeSql
{
    public interface IJsonDataAccess
    {
        bool WriteToJson<T>(T objectToWrite, string fileName);
        T GetByFileName<T>(string profileToReturn);
    }
}