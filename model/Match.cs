using System;
using System.Collections.Generic;

namespace model
{
    public class Match
    {
        public long Id { get; set; }
        public DateTime DebutPrevu { get; set; }
        public DateTime DebutReel { get; set; }
        public UserInfo Joueur1 { get; set; }
        public UserInfo Joueur2 { get; set; }
        public Account Arbitre { get; set; }
        
        public ICollection<Point> Score { get; set; }
    }
}