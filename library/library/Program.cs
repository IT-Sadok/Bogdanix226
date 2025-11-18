using library;

internal class Program
{
    public static void Main(string[] args)
    {
        LibraryService library = new LibraryService();

        while (true)
        {
            Console.Write("Choose an action: ");
            Console.WriteLine("1. Add book");
            Console.WriteLine("2. Delete a book");
            Console.WriteLine("3. Search for a book by id");
            Console.WriteLine("4. Show all books");
            Console.WriteLine("5. Borrow a book");
            Console.WriteLine("6. Return the book");
            Console.WriteLine("0. Exit");
            

            int choice = Convert.ToUInt16(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    var newBook = BookInfoMethods.GetBookInfo();
                    library.AddBook(newBook);
                    break;

                case 2:
                    Console.Write("Enter book ID to delete: ");
                    int deleteId = Convert.ToUInt16(Console.ReadLine());
                    Console.WriteLine(library.DeleteBook(deleteId)
                        ? "Book deleted."
                        : "Book not found.");
                    break;

                case 3:
                    Console.Write("Enter book name or author: ");
                    string query = Console.ReadLine();

                    var found = library.FindBook(query);

                    if (found == null)
                        Console.WriteLine("Book not found.");
                    else
                        PrintBook(found);

                    break;

                case 4:
                    var allBooks = library.GetAllBooks();
                    if (allBooks.Count == 0)
                        Console.WriteLine("No books available.");

                    foreach (var book in allBooks)
                        PrintBook(book);

                    break;

                case 5:
                    Console.Write("Enter ID to borrow: ");
                    int borrowId = Convert.ToUInt16(Console.ReadLine());
                    Console.WriteLine(library.BorrowBook(borrowId)
                        ? "Book borrowed!"
                        : "Cannot borrow this book.");
                    break;

                case 6:
                    Console.Write("Enter ID to return: ");
                    int returnId = Convert.ToUInt16(Console.ReadLine());
                    Console.WriteLine(library.ReturnBook(returnId)
                        ? "Book returned!"
                        : "Cannot return this book.");
                    break;

                case 0:
                    return;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }

    static void PrintBook(bookInfo book)
    {
        Console.WriteLine($"Name: {book.name}");
        Console.WriteLine($"Author: {book.author}");
        Console.WriteLine($"Year: {book.year}");
        Console.WriteLine($"ID: {book.id}");
        Console.WriteLine($"Status: {(book.IsBorrowed ? "Borrowed" : "Available")}");
    }
}
