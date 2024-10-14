using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifinanazas.God.Applicattion.Dtos.Domain
{
    public class MovementDto
    {
        public int id { get; set; } 
        public int gameId { get; set; }            
        public int roundId { get; set; }    
        public int playerId { get; set; } 
        public int moveOptionId { get; set; }
    }
}
