using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.API.Application.User.Queries.Get;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("{id}/address")]
        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            var response = await _mediator.Send(new GetRequest() {UserId = id });

            return Ok(response);
        }
    }
}
