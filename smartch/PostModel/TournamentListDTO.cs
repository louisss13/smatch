using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartch.PostModel
{
    public class TournamentListDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ClubId { get; set; }
        public Address Address { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public ETournamentState Etat { get; set; }

        public ICollection<long> ParticipantsId { get; set; }
        public ICollection<string> AdminsId { get; set; }

        public ICollection<long> MatchesId { get; set; }

        public TournamentListDTO() { }
        public TournamentListDTO(Tournament tournament)
        {
            Id = tournament.Id;
            Name = tournament.Name;
            ClubId = tournament.Club.Id;
           
            Address = tournament.Address;
            BeginDate = tournament.BeginDate;
            EndDate = tournament.EndDate;
            Etat = tournament.Etat;
            List<long> users = new List<long>();
            foreach(TournamentJoueur joueur in tournament.Participants)
            {
                users.Add(joueur.UserInfoId);
            }
            ParticipantsId = users;

            List<string> account = new List<string>();
            /*foreach (TournamentAdmin admin in tournament.Admins)
            {
                account.Add(admin.Account);
            }*/
            AdminsId = account;
        }
    }
}
