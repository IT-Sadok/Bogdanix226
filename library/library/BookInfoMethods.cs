using System.Globalization;

namespace library;

public class BookInfoMethods
{
    public static  bookInfo GetBookInfo()
    {
        var books = FileManager.ReadInfo();
        
        bookInfo book = new bookInfo();
            
        Console.WriteLine("Enter the name of book:");
        book.name = Convert.ToString(Console.ReadLine());
        Console.WriteLine("Enter the author of book:");
        book.author = Convert.ToString(Console.ReadLine());
        Console.WriteLine("Enter the year of book release:");
        book.year = Convert.ToInt16(Console.ReadLine());
        Console.WriteLine("Enter the id of book:");
        book.id = Convert.ToInt16(Console.ReadLine());
        books.Add(book);

        FileManager.SaveInfo(books);
        return book;

    }

   

}