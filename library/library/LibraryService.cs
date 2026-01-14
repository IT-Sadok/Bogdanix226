namespace library;

public class LibraryService : ILibraryService
{
    private IFileManager _fileManager;
    private List<BookInfo> _books;
    private InputBookInfo _inputBookInfo;
    
    private readonly object _lock = new();

    public LibraryService(IFileManager fileManager)
    {
        _fileManager = fileManager;
        _books = _fileManager.ReadInfo();
        _inputBookInfo = new InputBookInfo();
    }

    public void AddBook()
    {
        var book = _inputBookInfo.GetBookInfo();

        lock (_lock)
        {
            book.Id = _books.Any() ? _books.Max(b => b.Id) + 1 : 1;
            _books.Add(book);
            _fileManager.SaveInfo(_books);
        }
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
        lock (_lock)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null) return false;

            _books.Remove(book);
            _fileManager.SaveInfo(_books);
            return true;
        }
    }

    public bool BorrowBook(int id)
    {
        lock (_lock)
        {
            var book = _books.FirstOrDefault(b => b.Id == id && b.Status == BookStatus.Available);
            if (book == null) return false;

            book.Status = BookStatus.Borrowed;
            _fileManager.SaveInfo(_books);
            return true;
        }
    }
    
    public bool ReturnBook(int id)
    {
        lock (_lock)
        {
            var book = _books.FirstOrDefault(b => b.Id == id && b.Status == BookStatus.Borrowed);
            if (book == null) return false;

            book.Status = BookStatus.Available;
            _fileManager.SaveInfo(_books);
            return true;
        }
    }

    public void UpdateBookName(int id, string newName)
    {
        lock (_lock)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null) return;

            book.Name = newName;
            _fileManager.SaveInfo(_books);
        }
    }

    public void SimulateConcurrentUpdates()
    {
        Task[] tasks = new Task[100];

        for (int i = 0; i < 100; i++)
        {
            int taskId = i;

            tasks[i] = Task.Run(() =>
            {
                if (!_books.Any()) return;

                var rnd = new Random();
                int bookId;

                lock (_lock)
                {
                    bookId = _books[rnd.Next(_books.Count)].Id;
                }

                UpdateBookName(bookId, $"Updated by task {taskId}");
            });
        }

        Task.WaitAll(tasks);
    }

    
}
