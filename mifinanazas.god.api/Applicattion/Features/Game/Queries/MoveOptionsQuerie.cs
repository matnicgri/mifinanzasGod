using MediatR;
using Mifinanazas.God.Applicattion.Dtos.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifinanazas.God.Applicattion.Features.Game.Queries
{
    public class MoveOptionsQuerie : IRequest<ResultObject<List<MoveOptionsResDto>>>
    {
        public int id { get; set; }        
    }
}
