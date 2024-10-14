using Mapster;
using Microsoft.EntityFrameworkCore;
using Mifinanazas.God.Applicattion.Dtos.Domain;
using Mifinanazas.God.Applicattion.Dtos.Req;
using Mifinanazas.God.Applicattion.Dtos.Res;
using Mifinanazas.God.Applicattion.Interfaces.Repositories;
using Mifinanazas.God.Persistence.Context;
using Mifinanzas.God.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mifinanazas.God.Persistence.Repositories
{
    public class GameRepository: IGameRepository,IGenericRepository
    {
        private readonly GodDbContext _context;
        public GameRepository(GodDbContext context)
        {
            _context = context;
        }

        public async Task<GameDto> Get(int gameId)
        {
            GameDto res = new GameDto();
            Game? resGame= _context.Games.FirstOrDefault(x=>x.id==gameId);
            
            return new GameDto() { id=resGame.id,
                                    dateInit=resGame.dateInit,
                                    player1Id=resGame.player1Id,
                                    player2Id=resGame.player2Id
                                };
        }
        public async Task<int> Save(GameDto game)
        {
            Game g = new Game(){ player1Id= game.player1Id, player2Id = game.player2Id, dateInit=DateTime.Now };
            await _context.Games.AddAsync(g);
            await _context.SaveChangesAsync();

            return g.id;
        }
        public async Task<List<ScoreResDto>> GetScore(ScoreReqDto req)
        {
            List<ScoreResDto> result = new List<ScoreResDto>();

            result=_context.Totals.Where(x => x.gameId == req.gameId)
                .GroupBy(x => x.roundId)
                .Select(g => new
                {
                    roundId = g.Key, 
                    totalPlayer1 = g.Sum(x => x.player1Total),
                    totalPlayer2 = g.Sum(x => x.player2Total),
                    player1Id = g.FirstOrDefault().player1Id,
                    player2Id = g.FirstOrDefault().player2Id,
                    player1Name = g.FirstOrDefault().player1Name,
                    player2Name = g.FirstOrDefault().player2Name
                })
                .Select(y=>new ScoreResDto()
                    {                
                        roundId = y.roundId,
                        winPlayer = y.totalPlayer1 > y.totalPlayer2 ? y.player1Name:y.player2Name,
                        winPlayerId = y.totalPlayer1 > y.totalPlayer2 ? y.player1Id : y.player2Id,
                }).ToList();

            return result;
        }

        public async Task<int> Move(MoveReqDto req)
        {
            int actualRound = req.roundId;
            int nextRound = 0;

            Movements movement = new Movements()
            {
                GameId = req.gameId,
                MoveOptionId = req.moveOptionId,
                PlayerId = req.playerId,
                RoundId = req.roundId
            };

            _context.Movements.Add(movement);
            _context.SaveChanges();

            return movement.Id;
        }

        public async Task<List<MovementDto>> GetMoveByRound(int gameId, int roundId)
        {
            List<MovementDto> lResMove = new List<MovementDto>();

            lResMove=_context.Movements.
                        OrderBy(y => y.Id)
                        .Where(x=> x.RoundId==roundId)
                        .Where(x => x.GameId == gameId)
                        .Select(y=>new MovementDto() {
                                                                        id = y.Id,
                                                                        gameId = y.GameId,
                                                                        roundId = y.RoundId,                                                                        
                                                                        moveOptionId=y.MoveOptionId,    
                                                                        playerId = y.PlayerId
                                                                    }).ToList();

            return lResMove;
        }

        public async Task<List<MoveOptionsResDto>> MoveOptions(MoveOptionsReqDto req)
        {
            List<MoveOptionsResDto> res = _context.MoveOptions.Select(x => new MoveOptionsResDto()
            {
                id = x.id,
                description = x.description,
                killId = x.killId
            }).ToList();
            return res;
        }

        public async Task<int> SaveTotal(TotalDto total)
        {
            Totals g = new Totals() { 
                gameId=total.gameId,
                player1Id=total.player1Id,
                player1Total=total.player1Total,
                player1Name=total.player1Name,
                player2Id=total.player2Id,  
                player2Name=total.player2Name,
                player2Total  =total.player2Total,  
                roundId = total.roundId,
                
            };
            await _context.Totals.AddAsync(g);
            await _context.SaveChangesAsync();

            return g.id;
        }
    }
}
