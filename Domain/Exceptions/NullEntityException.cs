using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class NullEntityException : Exception 
    {
        public NullEntityException(string message) : base(message) { }

        public NullEntityException(string message,  Exception innerException) : base(message, innerException) { }
    }
}
