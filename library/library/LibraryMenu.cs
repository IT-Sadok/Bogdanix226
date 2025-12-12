using System;
using library;
public class LibraryMenu
{
    private readonly LibraryService _library;

    public LibraryMenu(LibraryService library)
    {
        _library = library;
    }

    public void Start()
    {
        while (true)
        {
            Console.WriteLine("Choose an action:");
            Console.WriteLine("1. Add book");
            Console.WriteLine("2. Delete a book");
            Console.WriteLine("3. Search for a book by name or author");
            Console.WriteLine("4. Show all books");
            Console.WriteLine("5. Borrow a book");
            Console.WriteLine("6. Return the book");
            Console.WriteLine("0. Exit");
            Console.Write("> ");

            int choice = Convert.ToUInt16(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddBook();
                    break;

                case 2:
                    DeleteBook();
                    break;

                case 3:
                    SearchBook();
                    break;

                case 4:
                    ShowAllBooks();
                    break;

                case 5:
                    BorrowBook();
                    break;

                case 6:
                    ReturnBook();
                    break;

                case 0:
                    return;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }

    private void AddBook()
    {
        var newBook = BookInfoMethods.GetBookInfo();
        _library.AddBook(newBook);
    }

    private void DeleteBook()
    {
        Console.Write("Enter book ID to delete: ");
        int id = Convert.ToUInt16(Console.ReadLine());
        Console.WriteLine(_library.DeleteBook(id)
            ? "Book deleted."
            : "Book not found.");
    }

    private void SearchBook()
    {
        Console.Write("Enter book name or author: ");
        string query = Console.ReadLine();

        var found = _library.FindBook(query);

        if (found == null)
            Console.WriteLine("Book not found.");
        else
            PrintBook(found);
    }

    private void ShowAllBooks()
    {
        var books = _library.GetAllBooks();

        if (books.Count == 0)
        {
            Console.WriteLine("No books available.");
            return;
        }

        foreach (var b in books)
            PrintBook(b);
    }

    private void BorrowBook()
    {
        Console.Write("Enter ID to borrow: ");
        int id = Convert.ToUInt16(Console.ReadLine());
        Console.WriteLine(_library.BorrowBook(id)
            ? "Book borrowed!"
            : "Cannot borrow this book.");
    }

    private void ReturnBook()
    {
        Console.Write("Enter ID to return: ");
        int id = Convert.ToUInt16(Console.ReadLine());
        Console.WriteLine(_library.ReturnBook(id)
            ? "Book returned!"
            : "Cannot return this book.");
    }

    private void PrintBook(BookInfo book)
    {
        Console.WriteLine($"Name: {book.name}");
        Console.WriteLine($"Author: {book.author}");
        Console.WriteLine($"Year: {book.year}");
        Console.WriteLine($"ID: {book.id}");
        Console.WriteLine($"Status: {(book.IsBorrowed ? "Borrowed" : "Available")}");
    }
}
