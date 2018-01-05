using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartch.PostModel
{
    public class MatchDTO
    {
        public long Id { get; set; }
        public int Phase { get; set; }
        public TimeSpan DebutPrevu { get; set; }
        
        public UserDTO Joueur1 { get; set; }
        public UserDTO Joueur2 { get; set; }
        public long Joueur1Id { get; set; }
        public long Joueur2Id { get; set; }
        public AccountDTO Arbitre { get; set; }
        public String Emplacement { get; set; }

        public ICollection<Point> Score { get; set; }

        public MatchDTO() { }
        public MatchDTO(Match match)
        {
            Id = match.Id;
            Phase = match.Phase;
            DebutPrevu = match.DebutPrevu;
           
            Joueur1 = new UserDTO(match.Joueur1);
            Joueur2 = new UserDTO(match.Joueur2);
            Emplacement = match.Emplacement;
            if (match.Joueur1 != null)
                Joueur1Id = match.Joueur1.Id;
            if (match.Joueur2 != null)
                Joueur2Id = match.Joueur2.Id;
            Arbitre = new AccountDTO(match.Arbitre);

            Score = match.Score;
        }

        public Match GetMacth()
        {
            return new Match()
            {
                Id = this.Id,
                Phase = this.Phase,
                DebutPrevu = this.DebutPrevu,

                Joueur1 = (this.Joueur1 != null) ? this.Joueur1.getUser() : null,
                Joueur2 = (this.Joueur2 != null) ? this.Joueur2.getUser() : null,
                Arbitre = (this.Arbitre != null) ? this.Arbitre.GetAccount() : null,
                Emplacement = Emplacement,

                Score = this.Score
            };
        }
    }
    
}
