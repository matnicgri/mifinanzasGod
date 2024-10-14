using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mifinanazas.God.Applicattion.Dtos.Res;
using Mifinanazas.God.Applicattion.Features.Game.Commands;
using Mifinanazas.God.Applicattion.Features.Game.Queries;

namespace Mifinanazas.God.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SaveGame([FromBody] GameCommand command)
        {
            ResultObject<int> result = await _mediator.Send(command);

            if (!result.success)
            {
                return BadRequest(result.error);
            }

            return Ok(result);
        }

        [HttpGet("score")]
        public async Task<IActionResult> GetScore([FromQuery] ScoreQuerie querie)
        {
            ResultObject<List<ScoreResDto>> result = await _mediator.Send(querie);

            if (!result.success)
            {
                return BadRequest(result.error);
            }

            return Ok(result);
        }
    }
}
