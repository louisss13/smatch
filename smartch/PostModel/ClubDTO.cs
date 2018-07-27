using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartch.PostModel
{
    public class ClubDTO
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public Address Adresse { get; set; }
        public String ContactMail { get; set; }
        public String Phone { get; set; }
        public List<UserInfo> Admins { get; set; } = new List<UserInfo>();
        public List<UserInfo> Members { get; set; } = new List<UserInfo>();

        public ClubDTO() { }
        public ClubDTO(Club club)
        {
            if(club == null)
            {
                return;
            }
            Name = club.Name;
            Id = club.Id;
            Adresse = club.Adresse;
            ContactMail = club.ContactMail;
            Phone = club.Phone;
            List<Account> admins = new List<Account>();
            /* foreach(ClubAdmins clubAdmin in club.Admins)
             {

             }*/
            List<UserInfo> membres = new List<UserInfo>();
            if(club.Members != null)
                foreach(ClubMember membre in club.Members)
                {
                    membres.Add(membre.UserInfo);
                }
            Members = membres;

        }
        
    }
}
