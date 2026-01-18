using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderModule.Application.Queries.GetOrder;
using StockModule.Application.Queries.GetStocks;

namespace BeymenCaseStudy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet()]
        public async Task<IActionResult> GetStocks()
        {
            var query = new GetStocksQuery();

            var stocks = await _mediator.Send(query);

            if (stocks == null)
            {
                return NotFound();
            }

            return Ok(stocks);
        }

    }
}
