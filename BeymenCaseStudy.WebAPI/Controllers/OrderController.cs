using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderModule.Application.Commands.CreateOrder;
using OrderModule.Application.Queries.GetOrder;
using OrderModule.Domain;

namespace BeymenCaseStudy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{orderNumber}")]
        public async Task<IActionResult> GetOrder(string orderNumber)
        {
            var query = new GetOrderQuery { OrderNumber = orderNumber};

            var order = await _mediator.Send(query);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand createOrderCommand, [FromServices] IValidator<CreateOrderCommand> validator)
        {
            var validationResult = await validator.ValidateAsync(createOrderCommand);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var order = await _mediator.Send(createOrderCommand);

            return Ok(order);

        }
    }
}
