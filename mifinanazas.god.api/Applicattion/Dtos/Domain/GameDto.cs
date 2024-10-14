using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifinanazas.God.Applicattion.Dtos.Req
{
    public class GameDto
    {
        public int id { get; set; } 
        public int player1Id { get; set; }      
        public int player2Id { get; set; }
        public DateTime dateInit { get; set; }

    }
}
