namespace library;

public class LibraryService
{
    private List<BookInfo> _books;

    public LibraryService()
    {
        _books = FileManager.ReadInfo();
    }

    public void AddBook(BookInfo book)
    {
        _books.Add(book);
        FileManager.SaveInfo(_books);
    }

    public List<BookInfo> GetAllBooks()
    {
        return _books.ToList();
    }

    public BookInfo FindBook(string query)
    {
        return _books.FirstOrDefault(b =>
            b.name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            b.author.Contains(query, StringComparison.OrdinalIgnoreCase));
    }

    public bool DeleteBook(int id)
    {
        var book = _books.FirstOrDefault(b => b.id == id);
        if (book == null) return false;

        _books.Remove(book);
        FileManager.SaveInfo(_books);
        return true;
    }

    public bool BorrowBook(int id)
    {
        var book = _books.FirstOrDefault(b => b.id == id);
        if (book == null || book.IsBorrowed) return false;

        book.IsBorrowed = true;
        FileManager.SaveInfo(_books);
        return true;
    }

    public bool ReturnBook(int id)
    {
        var book = _books.FirstOrDefault(b => b.id == id);
        if (book == null || !book.IsBorrowed) return false;

        book.IsBorrowed = false;
        FileManager.SaveInfo(_books);
        return true;
    }
}