using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifinanzas.God.Domain.Entitites
{
    public class Movements
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        [Required]
        [ForeignKey(nameof(Game))] 
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        [Required]
        public int RoundId { get; set; } 

        [Required]
        [ForeignKey(nameof(Player))]        
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        [Required]
        [ForeignKey(nameof(MoveOptions))]
        public int MoveOptionId { get; set; }
        public virtual MoveOptions MoveOptions { get; set; }

    }
}
