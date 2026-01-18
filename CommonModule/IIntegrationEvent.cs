using System;
using System.Collections.Generic;
using System.Text;

namespace CommonModule
{
    public interface IIntegrationEvent
    {
        public Guid Id { get; }
        public DateTime CreatedDate { get; }
    }
}
