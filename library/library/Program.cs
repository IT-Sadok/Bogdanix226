using System.Text.Json;
using library;


internal class Program
{
    public static void Main(string[] args)
    {
        int choice;
        
        Console.WriteLine("1. Add book");
        Console.WriteLine("2. Delete a book");
        Console.WriteLine("3. Search for a book");
        Console.WriteLine("4. Show all books");
        Console.WriteLine("5. Borrow a book");
        Console.WriteLine("6. Return the book");
        Console.WriteLine("Choose an action:");

        choice = Convert.ToInt16(Console.ReadLine());

        switch (choice)
         {
            
            case 1:
                BookInfoMethods.GetBookInfo();
                break;
            case 2:
                Console.WriteLine("Enter id");
                break;
            case 3:
                Console.WriteLine("Enter the name or author of book");
                break;
            case 4:
                var book =FileManager.ReadInfo();
                Console.WriteLine(book.name);
                Console.WriteLine(book.author);
                Console.WriteLine(book.year);
                Console.WriteLine(book.id);
                break;
            case 5:
                break;
            case 6:
                break;
            default:
                Console.WriteLine("Invalid choice. Please select from 1 to 6.");
                
                break;
         }
        

    }
}

