using library;

internal class Program
{
    public static void Main()
    {
        IFileManager fileManager = new FileManager();
        ILibraryService libraryService = new LibraryService(fileManager);

        libraryService.SimulateConcurrentUpdates();
        
        LibraryMenu menu = new LibraryMenu(libraryService);
        menu.Start();
    }
}