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
        for (int i = 0; i < _books.Count; i++)
        {
            if (_books[i].Id == id)
            {
                _books.RemoveAt(i);
                _fileManager.SaveInfo(_books);
                return true;
            }
        }
        return false;
    }

    public bool BorrowBook(int id)
    {
        for (int i = 0; i < _books.Count; i++)
        {
            if (_books[i].Id == id && !_books[i].IsBorrowed)
            {
                _books[i].IsBorrowed = true;
                _fileManager.SaveInfo(_books);
                return true;
            }
        }
        return false;
    }

    public bool ReturnBook(int id)
    {
        for (int i = 0; i < _books.Count; i++)
        {
            if (_books[i].Id == id && _books[i].IsBorrowed)
            {
                _books[i].IsBorrowed = false;
                _fileManager.SaveInfo(_books);
                return true;
            }
        }
        return false;
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

        Console.Write("Enter ID: ");
        int id;
        while(!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid ID. Enter again: ");
        }
        book.Id = id;

        return book;
    }
}
