using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mifinanazas.God.Applicattion.Dtos.Res;
using Mifinanazas.God.Applicattion.Features.Game.Commands;
using Mifinanazas.God.Applicattion.Features.Game.Queries;

namespace Mifinanazas.God.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovementsController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public MovementsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Movement([FromBody] MoveCommand command)
        {
            ResultObject<MoveResDto> result = await _mediator.Send(command);

            if (!result.success)
            {
                return BadRequest(result.error);
            }

            return Ok(result);
        }

        [HttpGet("options")]
        public async Task<IActionResult> GetMoveOptions([FromQuery] MoveOptionsQuerie querie)
        {
            ResultObject<List<MoveOptionsResDto>> result = await _mediator.Send(querie);

            if (!result.success)
            {
                return BadRequest(result.error);
            }

            return Ok(result);
        }

        [HttpPost("options")]
        public async Task<IActionResult> UpdateMoveOptions([FromBody] MoveOptionsCommand querie)
        {
            ResultObject<bool> result = await _mediator.Send(querie);

            if (!result.success)
            {
                return BadRequest(result.error);
            }

            return Ok(result);
        }
    }
}
