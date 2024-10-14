using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifinanzas.God.Domain.Entitites
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [ForeignKey("Player1")]
        public int player1Id { get; set; }
        [ForeignKey("Player2")]
        public int player2Id { get; set; }
        public DateTime dateInit { get; set; }
    }
}
