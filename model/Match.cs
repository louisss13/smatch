using System;

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
        public int pointJ1 { get; set; }
        public int pointJ2 { get; set; }
    }
}