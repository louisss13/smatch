using System;
using System.Collections.Generic;

namespace model
{
    public class Tournament
    {
        public enum State { EnCours, Fini, EnPreparation };
        public long Id { get; set; }
        public string Name { get; set; }
        public Club Club { get; set; }
        public Address Address { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public State Etat { get; set; }

        public ICollection<TournamentJoueur> Participants { get; set; }
        public ICollection<TournamentAdmin> Admins { get; set; }

        public ICollection<Match> Matches { get; set; }


    }
}