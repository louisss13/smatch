using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace model
{
    public class Club
    {
        public long ClubId { get; set; }
        public String Name { get; set; }
        public Address Adresse{ get; set; }
        public String ContactMail { get; set; }
        public String Phone { get; set; }
        public List<ClubAdmins> Admins { get; set; } = new List<ClubAdmins>();
        public List<ClubMember> Members { get; set; }
        public List<Tournament> Tournaments { get; set; }

        public Club() {
           
        }

        public override bool Equals(object obj)
        {
            var club = obj as Club;
            return club != null &&
                   ClubId == club.ClubId;
        }

        public override int GetHashCode()
        {
            return 1774844376 + ClubId.GetHashCode();
        }
    }
}
