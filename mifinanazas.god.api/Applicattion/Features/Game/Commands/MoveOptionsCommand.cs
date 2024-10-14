using MediatR;
using Mifinanazas.God.Applicattion.Dtos.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifinanazas.God.Applicattion.Features.Game.Commands
{
    public class MoveOption    
    {        
        public int id { get; set; }
        public string description { get; set; }
        public int killId { get; set; }
    }

    public class MoveOptionsCommand : IRequest<ResultObject<bool>>
    {
        public List<MoveOption> moveOptions { get; set; } = new List<MoveOption>();
    }
}
