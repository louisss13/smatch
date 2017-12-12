using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace model
{
    public class Tournament
    {
        private Club _club;
        public enum State { EnCours, Fini, EnPreparation };
        public long Id { get; set; }
        public string Name { get; set; }

        public long ClubId { get; set; }
        
        public Club Club {
            get
            {
                return _club;
            }
            set
            {
                _club = value;
                ClubId = _club.ClubId;
            }
        }
        public Address Address { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public State Etat { get; set; }

        public ICollection<TournamentJoueur> Participants { get; set; }
        public ICollection<TournamentAdmin> Admins { get; set; }

        public ICollection<Match> Matches { get; set; }

        public override bool Equals(object obj)
        {
            var tournament = obj as Tournament;
            return tournament != null &&
                   Id == tournament.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}