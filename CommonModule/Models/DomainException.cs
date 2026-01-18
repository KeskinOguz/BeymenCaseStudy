using System;
using System.Collections.Generic;
using System.Text;

namespace CommonModule.Models
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}
