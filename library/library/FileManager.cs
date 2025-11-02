using System.Diagnostics;
using System.Text.Json;
using System.Xml.Serialization;

namespace library;

public class FileManager
{
    private static string _filepath = "books.json";
    public static void SaveInfo(bookInfo bookInfo)
    {
        var json = JsonSerializer.Serialize(bookInfo);
        File.WriteAllText(_filepath, json);
    }
    
    public static bookInfo ReadInfo()
    {
        if (!File.Exists(_filepath))
        {
            Console.WriteLine("File not found");
            return null;
        }
        
        var jsonText = File.ReadAllText(_filepath);
        var book = JsonSerializer.Deserialize<bookInfo>(jsonText);
           
        return book;
    }
}