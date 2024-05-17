using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShoppingList.Helpers
{
    public static class FileHelper
    {
        private static string GetFilePath(string fileName)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(folderPath, fileName);
        }

        public static async Task SaveToFileAsync<T>(string fileName, List<T> data)
        {
            string filePath = GetFilePath(fileName);
            string json = JsonSerializer.Serialize(data);
            await File.WriteAllTextAsync(filePath, json);
        }

        public static async Task<List<T>> ReadFromFileAsync<T>(string fileName)
        {
            string filePath = GetFilePath(fileName);
            if (!File.Exists(filePath))
                return new List<T>();

            string json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<T>>(json);
        }
    }
}
