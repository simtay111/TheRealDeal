using System;
using System.IO;
using System.Web.Helpers;

namespace RecreateMeSql
{
    public class JsonDataAccess : IJsonDataAccess
    {
        public bool WriteToJson<T>(T objectToWrite, string fileName)
        {
            using (var writer = new StreamWriter(String.Format(@"C:\Test\{0}.txt", fileName)))
            {
                writer.AutoFlush = true;

                Json.Write(objectToWrite, writer);
            }

            return true;
        }

        public T GetByFileName<T>(string fileName)
        {
            string jsonText;

            using (var reader = new StreamReader(String.Format(@"C:\Test\{0}.txt", fileName)))
            {
                jsonText = reader.ReadLine();
            }

            return Json.Decode<T>(jsonText);
        }
    }
}