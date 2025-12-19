using library;

namespace library;

public class InputBookInfo
{
    public BookInfo GetBookInfo()
    {
        BookInfo book = new BookInfo();

        Console.Write("Enter the name: ");
        book.Name = Console.ReadLine();

        Console.Write("Enter the author: ");
        book.Author = Console.ReadLine();

        int year;
        Console.Write("Enter year: ");
        while (!int.TryParse(Console.ReadLine(), out year))
            Console.Write("Invalid year. Enter again: ");
        book.Year = year;

        return book;
    }
}