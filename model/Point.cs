
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace model
{
    public class Point
    {
        
        public long Id { get; set; }
        [Required]
        public EJoueurs Joueur { get; set; }
        [Required]
        public int Order { get; set; }
        public long MatchId { get; set; }
        
    }
}