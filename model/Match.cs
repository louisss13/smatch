using System;

namespace model
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime DebutPrevu { get; set; }
        public DateTime DebutReel { get; set; }
        public User Joueur1 { get; set; }
        public User Joueur2 { get; set; }
        public User Arbitre { get; set; }
        public int pointJ1 { get; set; }
        public int pointJ2 { get; set; }
    }
}