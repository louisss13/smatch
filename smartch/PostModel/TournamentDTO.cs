using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartch.PostModel
{
    public class TournamentDTO
    {
       
       
        public long Id { get; set; }
        public string Name { get; set; }

        public ClubDTO Club { get; set; }
       
        public Address Address { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public ETournamentState Etat { get; set; }

        public ICollection<UserDTO> Participants { get; set; }
        public ICollection<AccountDTO> Admins { get; set; }

        public ICollection<Match> Matches { get; set; }
        public TournamentDTO() { }
        public TournamentDTO(Tournament tournament)
        {
            Id = tournament.Id;
            Name = tournament.Name;
            Club = new ClubDTO(tournament.Club);

            Address = tournament.Address;
            BeginDate = tournament.BeginDate;
            EndDate = tournament.EndDate;
            Etat = tournament.Etat;
            List<UserDTO> users = new List<UserDTO>();
            foreach (TournamentJoueur joueur in tournament.Participants)
            {
                users.Add(new UserDTO(joueur.User));
            }
            Participants = users;

            List<AccountDTO> account = new List<AccountDTO>();
            /*foreach (TournamentAdmin admin in tournament.Admins)
            {
                account.Add(admin.Account);
            }*/
            Admins = account;
        }
    }
}
