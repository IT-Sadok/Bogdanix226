using System.Text.Json;

namespace library;

public class FileManager
{
    private static string _filepath = "books.json";

    public static void SaveInfo(List<BookInfo> books)
    {
        var json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filepath, json);
    }

    public static List<BookInfo> ReadInfo()
    {
        if (!File.Exists(_filepath))
            return new List<BookInfo>();

        string json = File.ReadAllText(_filepath);

        return JsonSerializer.Deserialize<List<BookInfo>>(json) 
               ?? new List<BookInfo>();
    }
}