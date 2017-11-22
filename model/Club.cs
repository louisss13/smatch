using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    public class Club
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public Address Adresse{ get; set; }
        public String ContactMail { get; set; }
        public String Phone { get; set; }
        public ICollection<ClubAdmin> Admins { get; set; }
        public ICollection<ClubMember> Members { get; set; }
        public ICollection<Tournament> Tournaments { get; set; }
    }
}
