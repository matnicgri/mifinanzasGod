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
    public class MoveOptionsQuerieHandler : IRequestHandler<MoveOptionsQuerie, ResultObject<List<MoveOptionsResDto>>>
    {        
        private readonly IGameRepository _gameRepository;
             
        public MoveOptionsQuerieHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }       
        public async Task<ResultObject<List<MoveOptionsResDto>>> Handle(MoveOptionsQuerie request, CancellationToken cancellationToken)
        {
            MoveOptionsReqDto req = request.Adapt<MoveOptionsReqDto>();
            var result = await _gameRepository.MoveOptions(req);

            return new ResultObject<List<MoveOptionsResDto>> { success = true, error = "", data = result };
        }
    }
}
