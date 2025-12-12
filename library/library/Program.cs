using library;

internal class Program
{
    public static void Main(string[] args)
    {
        LibraryService library = new LibraryService();
        LibraryMenu menu = new LibraryMenu(library);

        menu.Start();
    }
}