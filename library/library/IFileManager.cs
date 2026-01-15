namespace library;

public interface IFileManager
{
    void SaveInfo(List<BookInfo> books);
    List<BookInfo> ReadInfo();
}