using Domain.Entities.Common;

namespace Domain.Entities;

public class Book : BaseEntity
{
    public bool IsAvailable { get; set; }
    public DateTime UpdateTime { get; set; }
    public DateOnly PublicationDate { get; set; }
}
