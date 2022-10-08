namespace App.DataBase
{
    using System;
    using System.IO;
    using Newtonsoft.Json;


    public class DataBaseHelper
    {
        public void TextIntoType<T>(string txtPath, out T parsedResult)
        {
            string jsonText = File.ReadAllText(txtPath);
            parsedResult = JsonConvert.DeserializeObject<T>(jsonText);
        }

        public void TypeIntoText(string path, object obj)
        {
            var serializedText = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(path, serializedText);
        }

        public void WriteTextToFile(string path, string text)
        {
            File.WriteAllText(path, text);
        }

        public string ReadTextFromFile(string path)
        {
            return File.ReadAllText(path);
        }

        public void CreateDataBaseIfNeeded(string path, Action callback = null)
        {
            if (!File.Exists(path))
            {
                CreateFile(path);
                callback?.Invoke();
            }
        }

        public void CreateFile(string path)
        {
            StreamWriter writer = new StreamWriter(path, true);
            writer.Close();
        }
        
        public void SetBaseTypeText<T>(string path) where T : new()
        {
            var initialStructure = new T();
            TypeIntoText(path, initialStructure);
        }

        public void RemoveDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path,true);
            }
        }
    }
}