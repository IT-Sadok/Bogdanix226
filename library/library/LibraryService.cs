namespace library;

public class LibraryService : ILibraryService
{
    private  IFileManager _fileManager;
    private  List<BookInfo> _books;
    private  InputBookInfo _inputBookInfo;

    private  SemaphoreSlim _semaphore = new(1, 1);
    private static  Random _random = new();

    public LibraryService(IFileManager fileManager)
    {
        _fileManager = fileManager;
        _books = _fileManager.ReadInfo();
        _inputBookInfo = new InputBookInfo();
    }

    public void AddBook()
    {
        _semaphore.Wait();
        try
        {
            var book = _inputBookInfo.GetBookInfo();
            book.Id = _books.Any() ? _books.Max(b => b.Id) + 1 : 1;

            _books.Add(book);
            _fileManager.SaveInfo(_books);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public List<BookInfo> GetAllBooks() => new(_books);

    public BookInfo FindBook(string query)
        => _books.FirstOrDefault(b =>
            b.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            b.Author.Contains(query, StringComparison.OrdinalIgnoreCase));

    public bool DeleteBook(int id)
    {
        _semaphore.Wait();
        try
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null) return false;

            _books.Remove(book);
            _fileManager.SaveInfo(_books);
            return true;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public bool BorrowBook(int id)
    {
        _semaphore.Wait();
        try
        {
            var book = _books.FirstOrDefault(b => b.Id == id && b.Status == BookStatus.Available);
            if (book == null) return false;

            book.Status = BookStatus.Borrowed;
            _fileManager.SaveInfo(_books);
            return true;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public bool ReturnBook(int id)
    {
        _semaphore.Wait();
        try
        {
            var book = _books.FirstOrDefault(b => b.Id == id && b.Status == BookStatus.Borrowed);
            if (book == null) return false;

            book.Status = BookStatus.Available;
            _fileManager.SaveInfo(_books);
            return true;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task SimulateConcurrentUpdatesAsync()
    {
        Task[] tasks = new Task[100];

        for (int i = 0; i < tasks.Length; i++)
        {
            int taskId = i;

            tasks[i] = Task.Run(async () =>
            {
                int action = _random.Next(1, 5);

                await _semaphore.WaitAsync();
                try
                {
                    switch (action)
                    {
                        case 1: AddRandomBook(taskId); break;
                        case 2: DeleteRandomBook(); break;
                        case 3: UpdateRandomBook(taskId); break;
                        case 4: BorrowRandomBook(); break;
                        case 5: ReturnRandomBook(); break;
                    }

                    _fileManager.SaveInfo(_books);
                }
                finally
                {
                    _semaphore.Release();
                }
            });
        }

        await Task.WhenAll(tasks);
    }

    private void AddRandomBook(int taskId)
    {
        var book = new BookInfo
        {
            Id = _books.Any() ? _books.Max(b => b.Id) + 1 : 1,
            Name = $"Book added by task {taskId}",
            Author = $"Author {_random.Next(1, 100)}",
            Year = _random.Next(1990, 2025),
            Status = BookStatus.Available
        };

        _books.Add(book);
    }

    private void DeleteRandomBook()
    {
        if (!_books.Any()) return;

        var book = _books[_random.Next(_books.Count)];
        _books.Remove(book);
    }

    private void UpdateRandomBook(int taskId)
    {
        if (!_books.Any()) return;

        var book = _books[_random.Next(_books.Count)];
        book.Name = $"Updated by task {taskId}";
    }

    private void BorrowRandomBook()
    {
        var book = _books.FirstOrDefault(b => b.Status == BookStatus.Available);
        if (book != null)
            book.Status = BookStatus.Borrowed;
    }

    private void ReturnRandomBook()
    {
        var book = _books.FirstOrDefault(b => b.Status == BookStatus.Borrowed);
        if (book != null)
            book.Status = BookStatus.Available;
    }
}
