using System.Diagnostics;
using System.Text.Json;
using System.Xml.Serialization;

namespace library;

public class FileManager
{
    private static string _filepath = "books.json";
    public static void SaveInfo(List<bookInfo> bookInfo)
    {
        
        var json = JsonSerializer.Serialize(bookInfo);
        File.WriteAllText(_filepath, json);
    }
    
    public static List<bookInfo> ReadInfo()
    {
        if (!File.Exists(_filepath))
        {
            Console.WriteLine("File not found");
            return new List<bookInfo>();
        }
        
        var jsonText = File.ReadAllText(_filepath);
        var books = JsonSerializer.Deserialize<List<bookInfo>>(jsonText);
           
        return books;
    }
}