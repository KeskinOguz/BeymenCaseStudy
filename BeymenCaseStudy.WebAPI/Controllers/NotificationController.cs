using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationModule.Application.GetNotifications;
using StockModule.Application.Queries.GetStocks;

namespace BeymenCaseStudy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetNotificationQuery();

            var notificationLogs = await _mediator.Send(query);

            if (notificationLogs == null)
            {
                return NotFound();
            }

            return Ok(notificationLogs);
        }
    }
}
