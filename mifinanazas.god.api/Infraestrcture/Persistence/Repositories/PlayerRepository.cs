using Mapster;
using Microsoft.EntityFrameworkCore;
using Mifinanazas.God.Applicattion.Dtos.Req;
using Mifinanazas.God.Applicattion.Dtos.Res;
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
    public class PlayerRepository: IPlayerRepository, IGenericRepository
    {
        private readonly GodDbContext _context;
        public PlayerRepository(GodDbContext context)
        {
            _context = context;
        }

        public async Task<int> Save(PlayerReqDto player)
        {
            Player p = new Player() { name = player.name };
            await _context.Players.AddAsync(p);
            await _context.SaveChangesAsync();
            
            return p.id;           

        }
        public async Task<PlayerResDto> Get(string name)
        {
            var playerRes=_context.Players.FirstOrDefault(p => p.name.Equals(name));

            if (playerRes!=null)
            {
                return new PlayerResDto (){ 
                    id = playerRes.id,
                    name= playerRes.name
                };
            }
            else
            {
                return null;
            }            
        }

        public async Task<PlayerResDto> GetById(int id)
        {
            var playerRes = _context.Players.FirstOrDefault(p => p.id==id);

            if (playerRes != null)
            {
                return new PlayerResDto()
                {
                    id = playerRes.id,
                    name = playerRes.name
                };
            }
            else
            {
                return null;
            }
        }
    }
}
