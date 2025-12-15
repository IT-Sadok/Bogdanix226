namespace library;

public class InputBookInfo
{
    public  BookInfo GetBookInfo()
    {
        
        BookInfo book = new BookInfo();

        Console.Write("Enter the name: ");
        book.Name = Console.ReadLine();

        Console.Write("Enter the author: ");
        book.Author = Console.ReadLine();

        Console.Write("Enter year: ");
        book.Year = Convert.ToInt16(Console.ReadLine());

        Console.Write("Enter ID: ");
        book.Id = Convert.ToUInt16(Console.ReadLine());

        return book;
    }
}