using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mifinanazas.God.Applicattion.Dtos.Domain;
using Mifinanazas.God.Applicattion.Dtos.Req;
using Mifinanazas.God.Applicattion.Dtos.Res;
using Mifinanazas.God.Applicattion.Features.Game.Commands;
using Mifinanazas.God.Applicattion.Interfaces.Repositories;
using Mifinanazas.God.Persistence.Context;
using Mifinanzas.God.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mifinanazas.God.Persistence.Repositories
{
    public class MovementsRepository : IMovementsRepository, IGenericRepository
    {
        private readonly GodDbContext _context;
        public MovementsRepository(GodDbContext context)
        {
            _context = context;
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

            lResMove = _context.Movements.
                        OrderBy(y => y.Id)
                        .Where(x => x.RoundId == roundId)
                        .Where(x => x.GameId == gameId)
                        .Select(y => new MovementDto()
                        {
                            id = y.Id,
                            gameId = y.GameId,
                            roundId = y.RoundId,
                            moveOptionId = y.MoveOptionId,
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


        public async Task<bool> UpdateMovementsOptions(MoveOptionReqDto option)
        {
            var movement = await _context.MoveOptions.FindAsync(option.id);

            if (movement != null)
            {               
                movement.killId = option.killId;
                _context.MoveOptions.Update(movement);
            }
            else
            {              
                await _context.MoveOptions.AddAsync(new MoveOptions{                   
                    description=option.description,
                    killId = option.killId
                });
            }

            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Update(List<MoveOptionReqDto> options)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var option in options)
                {
                    var updated = await UpdateMovementsOptions(option);
                    if (!updated)
                    {
                        await transaction.RollbackAsync();
                        return false; 
                    }
                }

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
