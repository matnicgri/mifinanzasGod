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

namespace Mifinanazas.God.Applicattion.Features.Game.Commands
{
    public class GameCommandHandler : IRequestHandler<GameCommand, ResultObject<int>>
    {        
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;

        public GameCommandHandler(IGameRepository gameRepository, IPlayerRepository playerRepository)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
        }
        public async Task<ResultObject<int>> Handle(GameCommand request, CancellationToken cancellationToken)
        {
            GameReqDto req = request.Adapt<GameReqDto>();
            int player1Id = 0;
            int player2Id = 0;

            var player1= await _playerRepository.Get(req.player1Name);
            var player2 = await _playerRepository.Get(req.player2Name);

            if (player1 == null)
            {
                player1Id=await _playerRepository.Save(new PlayerReqDto() { id=-1,name= req.player1Name});
            }
            else
            {
                player1Id = player1.id;
            }

            if (player2 == null)
            {
                player2Id=await _playerRepository.Save(new PlayerReqDto() { id = -1, name = req.player2Name });
            }
            else
            {
                player2Id = player2.id;
            }

            GameDto gameSave = new GameDto() { 
            player1Id= player1Id,
            player2Id= player2Id
            };

            var result = await _gameRepository.Save(gameSave);

            return new ResultObject<int> { success=true,error="",data=result};
        }
    }
}
