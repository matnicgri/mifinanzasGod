using Mifinanazas.God.Applicattion.Dtos.Domain;
using Mifinanazas.God.Applicattion.Dtos.Req;
using Mifinanazas.God.Applicattion.Dtos.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifinanazas.God.Applicattion.Interfaces.Repositories
{
    public interface IGameRepository
    {
        public Task<int> Save(GameDto req);
        public Task<int> SaveTotal(TotalDto total);
        public Task<List<ScoreResDto>> GetScore(ScoreReqDto req);
        public Task<int> Move(MoveReqDto req);
        public Task<List<MoveOptionsResDto>> MoveOptions(MoveOptionsReqDto req);
        public Task<List<MovementDto>> GetMoveByRound(int gameId, int roundId);
        public Task<GameDto> Get(int gameId);
    }
}
