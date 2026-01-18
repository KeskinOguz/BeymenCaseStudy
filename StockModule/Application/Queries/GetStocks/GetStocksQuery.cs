using MediatR;
using StockModule.Domain;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace StockModule.Application.Queries.GetStocks
{
    public class GetStocksQuery : IRequest<IList<Stock>>
    {

    }
}
