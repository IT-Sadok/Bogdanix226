namespace library;

public class LibraryService : ILibraryService
{
    private  IFileManager _fileManager;
    private  List<BookInfo> _books;

    public LibraryService(IFileManager fileManager)
    {
        _fileManager = fileManager;
        _books = fileManager.ReadInfo();
    }

    public void AddBook(BookInfo book)
    {
        _books.Add(book);
        _fileManager.SaveInfo(_books);
    }

    public List<BookInfo> GetAllBooks() => _books.ToList();

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
        _fileManager.SaveInfo(_books);
        return true;
    }

    public bool BorrowBook(int id)
    {
        var book = _books.FirstOrDefault(b => b.id == id);
        if (book == null || book.IsBorrowed) return false;

        book.IsBorrowed = true;
        _fileManager.SaveInfo(_books);
        return true;
    }

    public bool ReturnBook(int id)
    {
        var book = _books.FirstOrDefault(b => b.id == id);
        if (book == null || !book.IsBorrowed)
            return false;

        book.IsBorrowed = false;
        _fileManager.SaveInfo(_books);
        return true;
    }
}