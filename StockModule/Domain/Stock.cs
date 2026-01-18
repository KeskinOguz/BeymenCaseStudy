using System;
using System.Collections.Generic;
using System.Text;

namespace StockModule.Domain
{
    public class Stock
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
