namespace library;

public class BookInfo
{
    public string Name { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public int Id { get; set; }
    public BookStatus Status { get; set; } = BookStatus.Available;

}