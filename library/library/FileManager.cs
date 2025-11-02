using System.Diagnostics;
using System.Text.Json;

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
        var jsonText = File.ReadAllText(_filepath);
        var book = JsonSerializer.Deserialize<bookInfo>(jsonText);
        return book;
    }
}