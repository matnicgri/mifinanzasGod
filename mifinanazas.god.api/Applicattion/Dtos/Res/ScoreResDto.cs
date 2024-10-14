using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifinanazas.God.Applicattion.Dtos.Res
{
    public class ScoreResDto
    {
        public int id { get; set; }           
        public int roundId { get; set; }      
        public int winPlayerId { get; set; } 
        public string winPlayer { get; set; }
    }
}
