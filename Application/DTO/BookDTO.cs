using Application.DTO.Common;

namespace Application.DTO
{
    public class BookDTO : BaseDTO
    {
        public bool IsAvailable { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
