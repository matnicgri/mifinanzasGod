using Mapster;
using MediatR;
using Mifinanazas.God.Applicattion.Dtos.Domain;
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
    public class MoveCommandHandler : IRequestHandler<MoveCommand, ResultObject<MoveResDto>>
    {        
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;

        public MoveCommandHandler(IGameRepository gameRepository, IPlayerRepository playerRepository)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
        }
        public async Task<ResultObject<MoveResDto>> Handle(MoveCommand request, CancellationToken cancellationToken)
        {
            int i = 1;
            int id = 0;
            int killIdActual = 0;
            int killIdPrev = 0;
            bool gameFinished = false;
            string playerWinner = "";
            int nextRoundId = request.roundId;
            List<PlayerResDto> playersWinMoves = new List<PlayerResDto>();
            PlayerResDto? playerCalcWinner = new PlayerResDto();
            MovementDto movePrev = new MovementDto();
            MoveReqDto req = request.Adapt<MoveReqDto>();            
            List<MovementDto> lMove = new List<MovementDto>();
            List<MoveOptionsResDto> moveOptions=new List<MoveOptionsResDto>();

            var game=await _gameRepository.Get(req.gameId);
            playersWinMoves.Add(new PlayerResDto()
            {
                id = game.player1Id,
                totalScore = 0,
                name = (await _playerRepository.GetById(game.player1Id)).name,
                turn = ((game.player1Id != req.playerId)|| req.playerId==-1),
                winner = false
            });
            playersWinMoves.Add(new PlayerResDto() { 
                id = game.player2Id, 
                totalScore = 0,
                name = (await _playerRepository.GetById(game.player2Id)).name,
                turn = (game.player2Id != req.playerId),
                winner = false
            });

            //verifcar inicio de juego
            if (!(req.roundId == 1 && req.playerId == -1 && req.moveOptionId == -1))
            {

                moveOptions = await _gameRepository.MoveOptions(new MoveOptionsReqDto());
                id = await _gameRepository.Move(req);
                lMove = await _gameRepository.GetMoveByRound(req.gameId,req.roundId);

                foreach (MovementDto move in lMove)
                {
                    movePrev = lMove.FirstOrDefault(x => x.id == (move.id - 1));

                    if (movePrev != null && (i % 2 == 0)) //si es par verificar quien gano esa tirada
                    {
                        //si el killId es el del anterior, mata el anterior
                        killIdActual = moveOptions.First(x => x.id == move.moveOptionId).killId;
                        if (killIdActual == movePrev.moveOptionId)
                        {
                            playersWinMoves.First(x => x.id == move.playerId).totalScore++;
                        }

                        //si el killId del anterior es el del actual mata el actual
                        killIdPrev = moveOptions.First(x => x.id == movePrev.moveOptionId).killId;
                        if (killIdPrev == move.moveOptionId)
                        {
                            playersWinMoves.First(x => x.id == movePrev.playerId).totalScore++;
                        }
                    }
                    i++;
                }

                playerCalcWinner = playersWinMoves.FirstOrDefault(x => x.totalScore == 1);
                if (playerCalcWinner != null)
                {
                    playersWinMoves.First(x => x.id == playerCalcWinner.id).winner = true;

                    //se graban totales delk round                
                    await _gameRepository.SaveTotal(new TotalDto()
                    {
                        gameId = req.gameId,
                        roundId = nextRoundId,
                        player1Id = playersWinMoves[0].id,
                        player1Name = playersWinMoves[0].name,
                        player1Total = playersWinMoves[0].totalScore,
                        player2Id = playersWinMoves[1].id,
                        player2Name = playersWinMoves[1].name,
                        player2Total = playersWinMoves[1].totalScore
                    });

                    nextRoundId++;
                }

                var totalRoundsWinners = await _gameRepository.GetScore(new ScoreReqDto() { gameId = req.gameId });

                var winner = totalRoundsWinners
                .GroupBy(x => new { x.winPlayerId, x.winPlayer })
                .Select(g => new PlayerResDto
                {
                    id = g.Key.winPlayerId,
                    name = g.Key.winPlayer,
                    totalScore = g.Count()
                })
                .Where(x => x.totalScore >= 3)
                .FirstOrDefault();

                if (winner != null && playerCalcWinner != null)
                {
                    gameFinished = true;
                    playerWinner = playersWinMoves.First(x => x.id == winner.id).name;
                }
            }
            var result = new MoveResDto()
            {
                gameFinished = gameFinished,
                gamePlayerWinner = playerWinner,
                nextRoundId = nextRoundId,
                players = playersWinMoves
            };

            return new ResultObject<MoveResDto> { success=true,error="",data=result};
        }
    }
}
