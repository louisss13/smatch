using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartch.PostModel
{
    public class PointDTO
    {
        public long Id { get; set; }
        public EJoueurs Joueur { get; set; }
        public int Value { get; set; }
        public int Order { get; set; }
        public long MatchId { get; set; }
    }
}
