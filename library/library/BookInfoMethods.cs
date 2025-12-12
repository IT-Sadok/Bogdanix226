namespace library;

public class BookInfoMethods: IBookInfoMethods
{
    public  BookInfo GetBookInfo()
    {
        BookInfo book = new BookInfo();

        Console.Write("Enter the name: ");
        book.name = Console.ReadLine();

        Console.Write("Enter the author: ");
        book.author = Console.ReadLine();

        Console.Write("Enter year: ");
        book.year = Convert.ToInt16(Console.ReadLine());

        Console.Write("Enter ID: ");
        book.id = Convert.ToUInt16(Console.ReadLine());

        return book;
    }
}