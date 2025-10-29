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
                GetBookInfo();
                break;
            case 2:
                Console.WriteLine("Enter id");
                break;
            case 3:
                Console.WriteLine("Enter the name or author of book");
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            default:
                Console.WriteLine("Invalid choice. Please select from 1 to 6.");
                break;
         }
        
        
        
        static bookInfo GetBookInfo()
        {
            bookInfo book = new bookInfo();
            
            Console.WriteLine("Enter the name of book:");
            book.name = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter the author of book:");
            book.author = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter the year of book release:");
            book.year = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Enter the id of book:");
            book.id = Convert.ToInt16(Console.ReadLine());
     
            return book;
        }


    }
}

