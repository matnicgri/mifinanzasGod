using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifinanazas.God.Applicattion.Dtos.Res
{
    public class MoveResDto
    {
        public bool gameFinished { get; set; }           
        public string gamePlayerWinner { get; set; }      
        public int nextRoundId { get; set; } 
        public List<PlayerResDto> players { get; set; }
    }

    public class PlayerResDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public int totalScore { get; set; }
        public bool turn { get; set; }
        public bool winner { get; set; }
    }
}
