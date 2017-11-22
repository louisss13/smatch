using System;
using System.Collections.Generic;

namespace model
{
    public class User
    {
        public int Id { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public String FirstName { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public Address Adresse { get; set; }
        public DateTime birthday { get; set; }
        public DateTime dateInscription { get; set;}
        public DateTime dateDerniereConnection { get; set; }
        public int Active { get; set; }

        public ICollection<ClubAdmin> ClubAdmins { get; set; }
        public ICollection<ClubMember> ClubMembers { get; set; }

    }
}
