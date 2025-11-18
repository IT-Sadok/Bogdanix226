using System.Text.Json;

namespace library;

public class FileManager
{
    private static string _filepath = "books.json";

    public static void SaveInfo(List<bookInfo> books)
    {
        var json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filepath, json);
    }

    public static List<bookInfo> ReadInfo()
    {
        if (!File.Exists(_filepath))
            return new List<bookInfo>();

        string json = File.ReadAllText(_filepath);

        return JsonSerializer.Deserialize<List<bookInfo>>(json) 
               ?? new List<bookInfo>();
    }
}