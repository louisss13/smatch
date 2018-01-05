using model.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace model
{
    public class Tournament
    {
        private Club _club;

        public long Id { get; set; }
       
        
        [Required]
        public string Name { get; set; }
        
        public Club Club {
            get
            {
                return _club;
            }
            set
            {
                _club = value;
                
            }
        }
        [Required]
        public Address Address { get; set; }

        [DateBiggerOrSmallerThan("BeginDate", "EndDate")]
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public ETournamentState Etat { get; set; }

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