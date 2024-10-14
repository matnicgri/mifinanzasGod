using MediatR;
using Mifinanazas.God.Applicattion.Dtos.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifinanazas.God.Applicattion.Features.Game.Commands
{
    public class GameCommand : IRequest<ResultObject<int>>
    {
        public int gameId { get; set; }
        public string player1Name { get; set; }
        public string player2Name { get; set; }
    }
}
