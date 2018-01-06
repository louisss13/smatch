using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    public class Score
    {
        public List<EJoueurs> Joueurs { get; set; }
        public List<Dictionary<EJoueurs, int>> PointLevel { get; set; }
    }
}
