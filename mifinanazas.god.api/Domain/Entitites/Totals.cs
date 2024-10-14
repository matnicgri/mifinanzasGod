using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifinanzas.God.Domain.Entitites
{
    public class Totals
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [ForeignKey(nameof(game))]
        public int gameId { get; set; }
        public virtual Game game { get; set; } 
        public int roundId { get; set; }        
        public int player1Id { get; set; }
        public string player1Name { get; set; }  
        public int player1Total { get; set; }       
        public int player2Id { get; set; }       
        public string player2Name { get; set; }  
        public int player2Total { get; set; }
    }

}
