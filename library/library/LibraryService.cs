namespace library;

public class LibraryService : ILibraryService
{
    private IFileManager _fileManager;
    private List<BookInfo> _books;

    public LibraryService(IFileManager fileManager)
    {
        _fileManager = fileManager;
        _books = _fileManager.ReadInfo();
    }

    public void AddBook()
    {
        BookInfo book = GetBookInfoFromUser();
        book.Id = _books.Any() ? _books.Max(b => b.Id) + 1 : 1; 
        _books.Add(book);
        _fileManager.SaveInfo(_books);
    }

    public List<BookInfo> GetAllBooks() => new List<BookInfo>(_books);

    public BookInfo FindBook(string query)
    {
        foreach (var b in _books)
        {
            if (b.Name.ToLower().Contains(query.ToLower()) ||
                b.Author.ToLower().Contains(query.ToLower()))
            {
                return b;
            }
        }
        return null;
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

    private BookInfo GetBookInfoFromUser()
    {
    
        BookInfo book = new BookInfo();

        Console.Write("Enter book name: ");
        book.Name = Console.ReadLine();

        Console.Write("Enter author: ");
        book.Author = Console.ReadLine();

        Console.Write("Enter year: ");
        int year;
        while(!int.TryParse(Console.ReadLine(), out year))
        {
            Console.Write("Invalid year. Enter again: ");
        }
        book.Year = year;

        return book;
    }
}
