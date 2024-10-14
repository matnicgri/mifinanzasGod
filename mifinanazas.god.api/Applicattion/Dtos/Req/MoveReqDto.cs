using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifinanazas.God.Applicattion.Dtos.Req
{
    public class MoveReqDto
    {
        public int gameId { get; set; }
        public int roundId { get; set; }
        public int playerId { get; set; }
        public int moveOptionId { get; set; }

    }
}
