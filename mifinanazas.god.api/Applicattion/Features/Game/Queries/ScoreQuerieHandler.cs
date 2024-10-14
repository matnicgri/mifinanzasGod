using Mapster;
using MediatR;
using Mifinanazas.God.Applicattion.Dtos.Req;
using Mifinanazas.God.Applicattion.Dtos.Res;
using Mifinanazas.God.Applicattion.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifinanazas.God.Applicattion.Features.Game.Queries
{
    public class ScoreQuerieHandler : IRequestHandler<ScoreQuerie, ResultObject<List<ScoreResDto>>>
    {        
        private readonly IGameRepository _gameRepository;
             
        public ScoreQuerieHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }       
        public async Task<ResultObject<List<ScoreResDto>>> Handle(ScoreQuerie request, CancellationToken cancellationToken)
        {
            ScoreReqDto req = request.Adapt<ScoreReqDto>();
            var result = await _gameRepository.GetScore(req);

            return new ResultObject<List<ScoreResDto>> { success = true, error = "", data = result };
        }
    }
}
