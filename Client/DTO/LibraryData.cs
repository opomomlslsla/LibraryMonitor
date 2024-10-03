namespace Client.DTO;


public class LibraryData
{
    public int AvailableBooksCount { get; set; }
    public int TotalBooksCount { get; set; }
    public List<BookDTO> Books { get; set; }

}
