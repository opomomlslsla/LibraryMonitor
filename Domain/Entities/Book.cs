using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Book : BaseEntity
    {
        public bool IsAvailable { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateOnly PublicationDate { get; set; }
    }
}
