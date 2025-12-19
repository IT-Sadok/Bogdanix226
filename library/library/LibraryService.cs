namespace library;

public class LibraryService : ILibraryService
{
    private IFileManager _fileManager;
    private List<BookInfo> _books;
    private InputBookInfo _inputBookInfo;

    public LibraryService(IFileManager fileManager)
    {
        _fileManager = fileManager;
        _books = _fileManager.ReadInfo();
        _inputBookInfo = new InputBookInfo();
    }

    public void AddBook()
    {
        BookInfo book = _inputBookInfo.GetBookInfo();
        book.Id = _books.Any() ? _books.Max(b => b.Id) + 1 : 1; 
        _books.Add(book);
        _fileManager.SaveInfo(_books);
    }

    public List<BookInfo> GetAllBooks() => new List<BookInfo>(_books);

    public BookInfo FindBook(string query)
    {
        return _books.FirstOrDefault(b =>
            b.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            b.Author.Contains(query, StringComparison.OrdinalIgnoreCase));
    }


    public bool DeleteBook(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book == null) return false;

        _books.Remove(book);
        _fileManager.SaveInfo(_books);
        return true;
    }

    public bool BorrowBook(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id && b.Status == BookStatus.Available);
        if (book == null) return false;

        book.Status = BookStatus.Borrowed;

        _fileManager.SaveInfo(_books);
        return true;
    }

    public bool ReturnBook(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id && b.Status == BookStatus.Borrowed);
        if (book == null) return false;

        book.Status = BookStatus.Available;

        _fileManager.SaveInfo(_books);
        return true;
    }
    
}
