using MediatR;
using StockModule.Domain;
using StockModule.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockModule.Application.Queries.GetStocks
{
    public class GetStocksQueryHandler : IRequestHandler<GetStocksQuery, IList<Stock>>
    {
        private readonly IStockRepository _stockRepository;
        public GetStocksQueryHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        public async Task<IList<Stock>> Handle(GetStocksQuery request, CancellationToken cancellationToken)
        {
            var stocks = await _stockRepository.GetAllAsync();

            return stocks.ToList();
        }
    }
}
