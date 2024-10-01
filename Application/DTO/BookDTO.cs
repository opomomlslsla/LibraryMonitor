using Application.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class BookDTO : BaseDTO
    {
        public bool IsAvailable { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
