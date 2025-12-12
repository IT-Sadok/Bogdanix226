namespace library;

public class BookInfo
{
    public string Name { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public int Id { get; set; }
    public bool IsBorrowed { get; set; } = false;
}