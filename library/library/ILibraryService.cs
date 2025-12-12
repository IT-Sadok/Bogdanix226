namespace library;

public interface ILibraryService
{
    void AddBook(BookInfo book);
    List<BookInfo> GetAllBooks();
    BookInfo FindBook(string query);
    bool DeleteBook(int id);
    bool BorrowBook(int id);
    bool ReturnBook(int id);
}