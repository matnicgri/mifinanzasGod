using Mifinanazas.God.Applicattion.Dtos.Req;
using Mifinanazas.God.Applicattion.Dtos.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Mifinanazas.God.Applicattion.Interfaces.Repositories
{
    public interface IPlayerRepository
    {
        public Task<int> Save(PlayerReqDto player);
        public Task<PlayerResDto> Get(string name);
        public Task<PlayerResDto> GetById(int id);

    }
}
