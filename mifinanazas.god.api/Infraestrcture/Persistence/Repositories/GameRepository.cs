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
                        winPlayer = ( (y.totalPlayer1 > y.totalPlayer2) ? y.player1Name
                        : (y.totalPlayer1 < y.totalPlayer2) ? y.player2Name: ""),
                        winPlayerId = ( (y.totalPlayer1 > y.totalPlayer2) ? y.player1Id
                        : (y.totalPlayer1 < y.totalPlayer2) ? y.player2Id: -1)
                }).ToList();

            return result;
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
