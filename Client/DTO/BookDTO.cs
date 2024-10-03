namespace Client.DTO;

public class BookDTO 
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime UpdateTime { get; set; }
}
