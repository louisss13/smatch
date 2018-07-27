using Business;
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

        public ICollection<MatchDTO> Matches { get; set; }
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
            Participants = ToUserDTO(tournament.Participants);

            List<AccountDTO> account = new List<AccountDTO>();
            /*foreach (TournamentAdmin admin in tournament.Admins)
            {
                account.Add(admin.Account);
            }*/
            Admins = account;
            Matches = ToMatchesDTO(tournament.Matches);
        }

        private ICollection<MatchDTO> ToMatchesDTO(ICollection<Match> matches)
        {

            List<MatchDTO> matchs = new List<MatchDTO>();
            if (matches == null)
                return matchs;
            foreach (Match match in matches)
            {
                matchs.Add(new MatchDTO(match, null, new CalculPointPingPong()));
            }
            return  matchs;
        }
        private ICollection<UserDTO> ToUserDTO(ICollection<TournamentJoueur> participants)
        {
            if (participants == null)
                return null;
            List<UserDTO> users = new List<UserDTO>();
            foreach (TournamentJoueur joueur in participants)
            {
                users.Add(new UserDTO(joueur.User));
            }
            return users;
        }
    }
}
