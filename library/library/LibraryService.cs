using System.Collections.Generic;
using System.Linq;

namespace library
{
    public class LibraryService
    {
        private List<bookInfo> _books;

        public LibraryService()
        {
            _books = FileManager.ReadInfo();
        }

        public void AddBook(bookInfo book)
        {
            _books.Add(book);
            FileManager.SaveInfo(_books);
        }

        public List<bookInfo> GetAllBooks()
        {
            return _books;
        }

        public bookInfo FindBook(string query)
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
}