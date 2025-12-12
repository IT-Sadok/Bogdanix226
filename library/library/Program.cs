using library;

internal class Program
{
    public static void Main()
    {
        IFileManager fileManager = new FileManager();
        ILibraryService libraryService = new LibraryService(fileManager);
        IBookInfoMethods bookInfoMethods = new BookInfoMethods();

        LibraryMenu menu = new LibraryMenu(libraryService, bookInfoMethods);
        menu.Start();
    }
}